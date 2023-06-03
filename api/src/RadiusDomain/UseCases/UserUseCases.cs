using System.Collections.Immutable;
using RadiusDomain.DTOs.User;
using RadiusDomain.Entities;
using RadiusDomain.Exceptions;
using RadiusDomain.Factories.Interfaces;
using RadiusDomain.Repositories.Interfaces;
using RadiusDomain.UseCases.Interfaces;
using RadiusDomain.ValueObjects;

namespace RadiusDomain.UseCases;

public class UserUseCases : IUserUseCases
{
    private readonly IUserFactory _userFactory;

    private readonly IRadiusAttributeFactory _radiusAttributeFactory;

    private readonly IUserGroupFactory _userGroupFactory;

    private readonly IUserRepository _userRepository;

    public UserUseCases(IUserFactory userFactory, IRadiusAttributeFactory radiusAttributeFactory,
        IUserGroupFactory userGroupFactory, IUserRepository userRepository)
    {
        _userFactory = userFactory;
        _radiusAttributeFactory = radiusAttributeFactory;
        _userRepository = userRepository;
        _userGroupFactory = userGroupFactory;
    }

    public void Push(List<UserPushInDto> users)
    {
        var duplicatesUsers = _searchDuplicatesUsers(users);

        if (duplicatesUsers.Any())
        {
            var errorsSet = duplicatesUsers.Select(username => new KeyValuePair<string, EntityConflictException[]>(
                username,
                new[]
                {
                    new EntityConflictException(new Dictionary<string, ErrorMessage[]>
                        { ["key"] = new[] { ErrorCatalog.DuplicateEntity } }.ToImmutableDictionary())
                }));

            var errors = new Dictionary<string, EntityConflictException[]>(errorsSet);

            throw new EntitiesConflictException(errors.ToImmutableDictionary());
        }

        var sentinelAttrs = new List<RadiusAttribute> { new RadiusAttribute() };
        var createdUsers = new List<User>();

        var usersErrors = new Dictionary<string, EntityValidationException[]>();

        foreach (var user in users)
        {
            var userPropertiesErrors = new List<EntityValidationException>();

            var createdAttributes = new List<RadiusAttribute>();

            foreach (var attr in user.Attributes)
            {
                try
                {
                    var createdAttr = _radiusAttributeFactory.Create(attr.Name, attr.Op, attr.Value);
                    createdAttr.Owner = user.Username;

                    if (!userPropertiesErrors.Any() && !usersErrors.Any())
                    {
                        createdAttributes.Add(createdAttr);
                    }
                }
                catch (EntityValidationException e)
                {
                    userPropertiesErrors.Add(e);
                }
            }

            var createdUserGroups = new List<UserGroup>();

            foreach (var uGp in user.Groups)
            {
                Group? group = new Group();

                if (group == null)
                {
                    var errorsSet = new Dictionary<string, ErrorMessage>()
                    {
                        ["Name"]
                    };
                    
                    var exception = new EntityValidationException();
                    continue;
                }
                
                try
                {
                    var createdUserGroup = _userGroupFactory.Create(user.Username, group.Name, uGp.Priority);

                    if (!userPropertiesErrors.Any() && !usersErrors.Any())
                    {
                        createdUserGroups.Add(createdUserGroup);
                    }
                }
                catch (EntityValidationException e)
                {
                    userPropertiesErrors.Add(e);
                }
            }

            try
            {
                var userCreated = _userFactory.Create(user.Username,
                    !userPropertiesErrors.Any() ? createdAttributes : sentinelAttrs,
                    createdUserGroups);

                if (!userPropertiesErrors.Any() && !usersErrors.Any())
                {
                    createdUsers.Add(userCreated);
                }
            }
            catch (EntityValidationException e)
            {
                userPropertiesErrors.Add(e);
            }

            if (userPropertiesErrors.Any())
            {
                usersErrors.Add(user.Username, userPropertiesErrors.ToArray());
            }
        }

        if (usersErrors.Any())
        {
            throw new EntitiesValidationsException(usersErrors.ToImmutableDictionary());
        }

        _userRepository.InsertMany(createdUsers);
    }

    private static string[] _searchDuplicatesUsers(IEnumerable<UserPushInDto> users)
    {
        var allUsernames = new HashSet<string>();
        var duplicatesUsernames = new HashSet<string>();

        foreach (var username in users.Select(user => user.Username))
        {
            if (!allUsernames.Contains(username))
            {
                allUsernames.Add(username);
                continue;
            }

            duplicatesUsernames.Add(username);
        }

        return duplicatesUsernames.ToArray();
    }
}