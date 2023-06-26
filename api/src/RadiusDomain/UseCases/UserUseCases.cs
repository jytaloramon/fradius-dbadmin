using System.Collections.Immutable;
using RadiusDomain.DTOs.User;
using RadiusDomain.Entities;
using RadiusDomain.Exceptions;
using RadiusDomain.Factories.Interfaces;
using RadiusDomain.Repositories.Interfaces;
using RadiusDomain.UseCases.Interfaces;

namespace RadiusDomain.UseCases;

public class UserUseCases : IUserUseCases
{
    private readonly IUserFactory _userFactory;

    private readonly IRadiusAttributeFactory _radiusAttributeFactory;

    private readonly IUserGroupFactory _userGroupFactory;

    private readonly IUserRepository _userRepository;

    private readonly IGroupRepository _groupRepository;

    private const int MaxTask = 100;

    public UserUseCases(IUserFactory userFactory, IRadiusAttributeFactory radiusAttributeFactory,
        IUserGroupFactory userGroupFactory, IUserRepository userRepository, IGroupRepository groupRepository)
    {
        _userFactory = userFactory;
        _radiusAttributeFactory = radiusAttributeFactory;
        _userGroupFactory = userGroupFactory;
        _userRepository = userRepository;
        _groupRepository = groupRepository;
    }

    public async Task Push(List<UserPushInDto> users)
    {
        var duplicatesUsers = _searchDuplicatesUsers(users);

        if (duplicatesUsers.Any())
        {
            var errorsSet = duplicatesUsers.Select(username =>
                new EntityConflictException(new Dictionary<string, string>() { ["Username"] = username }
                    .ToImmutableDictionary()));

            throw new EntitiesConflictException(errorsSet.ToArray());
        }

        var createdUsers = new List<User>();
        var usersErrors = new List<EntityValidationException>();

        Task Run(UserPushInDto u)
        {
            try
            {
                var user = _createUser(u);

                if (!usersErrors.Any()) createdUsers.Add(user);
            }
            catch (EntityValidationException e)
            {
                usersErrors.Add(e);
            }

            return Task.CompletedTask;
        }

        for (var i = 0; i < users.Count; i += MaxTask)
        {
            var usersTask = new List<Task>();

            for (var j = 0; j < int.Min(MaxTask, users.Count - i); j++) usersTask.Add(Run(users[i + j]));

            try
            {
                await Task.WhenAll(usersTask);
            }
            catch (Exception)
            {
                Console.WriteLine("tete");
            }
        }

        if (usersErrors.Any()) throw new EntitiesValidationsException(usersErrors.ToArray());

        //_userRepository.InsertMany(createdUsers);
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

    private User _createUser(UserPushInDto user)
    {
        var createdAttributes = new List<RadiusAttribute>();
        var userAttributesErrors = new List<EntityValidationException>();

        foreach (var attr in user.Attributes)
        {
            try
            {
                var createdAttr = _radiusAttributeFactory.Create(attr.Name, attr.Op, attr.Value);

                if (userAttributesErrors.Any()) continue;

                createdAttr.Owner = user.Username;
                createdAttributes.Add(createdAttr);
            }
            catch (EntityValidationException e)
            {
                userAttributesErrors.Add(e);
            }
        }

        var createdUserGroups = new List<UserGroup>();
        var userGroupsErrors = new List<EntityValidationException>();

        foreach (var uGp in user.Groups)
        {
            var group = _groupRepository.GetByKey(uGp.Name);

            if (group == null)
            {
                var error = new EntityValidationException(new Dictionary<string, object>()
                    { ["Key"] = uGp.Name, ["Error"] = "Not Found" }.ToImmutableDictionary());

                userGroupsErrors.Add(error);
                continue;
            }

            try
            {
                var createdUserGroup = _userGroupFactory.Create(uGp.Priority);

                if (userGroupsErrors.Any() || userAttributesErrors.Any()) continue;

                createdUserGroup.UserUsername = user.Username;
                createdUserGroup.GroupName = uGp.Name;
                createdUserGroups.Add(createdUserGroup);
            }
            catch (EntityValidationException e)
            {
                userGroupsErrors.Add(e);
            }
        }

        EntityValidationException? userError = null;
        try
        {
            var attributesToValidation = !userAttributesErrors.Any()
                ? createdAttributes
                : new List<RadiusAttribute> { new() };

            var userCreated = _userFactory.Create(user.Username, attributesToValidation);

            if (!userGroupsErrors.Any() && !userAttributesErrors.Any())
            {
                userCreated.Groups = createdUserGroups;

                return userCreated;
            }
        }
        catch (EntityValidationException e)
        {
            userError = e;
        }

        var userErrorDict = userError != null
            ? userError.Errors.ToDictionary(u => u.Key, u => u.Value)
            : new Dictionary<string, object>();

        if (userAttributesErrors.Any()) userErrorDict.Add("Attributes", userAttributesErrors.ToArray());

        if (userGroupsErrors.Any()) userErrorDict.Add("Groups", userGroupsErrors.ToArray());

        throw new EntityValidationException(userErrorDict.ToImmutableDictionary());
    }
}