using System.Collections.Immutable;
using Moq;
using RadiusDomain.DTOs.Attribute;
using RadiusDomain.DTOs.User;
using RadiusDomain.Entities;
using RadiusDomain.Exceptions;
using RadiusDomain.Factories.Interfaces;
using RadiusDomain.Repositories.Interfaces;
using RadiusDomain.UseCases;

namespace RadiusDomain.UnitTests.UseCases;

public class UserUseCasesTests
{
    [Fact]
    public async Task Push_DuplicateUsers_ThrowEntitiesConflictException()
    {
        var useCases = new UserUseCases(new Mock<IUserFactory>().Object, new Mock<IRadiusAttributeFactory>().Object,
            new Mock<IUserGroupFactory>().Object, new Mock<IUserRepository>().Object,
            new Mock<IGroupRepository>().Object);

        var userList = new List<UserPushInDto>()
        {
            new UserPushInDto("user1", new List<AttributeDto>(), new List<UserGroupDto>()),
            new UserPushInDto("user2", new List<AttributeDto>(), new List<UserGroupDto>()),
            new UserPushInDto("user3", new List<AttributeDto>(), new List<UserGroupDto>()),
            new UserPushInDto("user1", new List<AttributeDto>(), new List<UserGroupDto>()),
            new UserPushInDto("user2", new List<AttributeDto>(), new List<UserGroupDto>()),
        };

        var actualExcept =
            await Assert.ThrowsAsync<EntitiesConflictException>(async () => await useCases.Push(userList));

        var actualUsernames = actualExcept.Errors.Select(e => e.Errors["Username"]).ToArray();
        Array.Sort(actualUsernames);

        Assert.Equal(2, actualUsernames.Length);
        Assert.Equal(new[] { "user1", "user2" }, actualUsernames);
    }

    [Fact]
    public async Task Push_HandlingUserFactoryException_ThrowEntitiesValidationsException()
    {
        var userFactMock = new Mock<IUserFactory>();
        userFactMock.Setup(factory => factory.Create(It.IsAny<string>(), It.IsAny<List<RadiusAttribute>>()))
            .Throws(new EntityValidationException(new Dictionary<string, object>() { ["Username"] = "fail" }
                .ToImmutableDictionary()));

        var useCases = new UserUseCases(userFactMock.Object, new Mock<IRadiusAttributeFactory>().Object,
            new Mock<IUserGroupFactory>().Object, new Mock<IUserRepository>().Object,
            new Mock<IGroupRepository>().Object);

        var userList = new List<UserPushInDto>()
        {
            new("user1", new List<AttributeDto>(), new List<UserGroupDto>()),
            new("user2", new List<AttributeDto>(), new List<UserGroupDto>())
        };

        var actualExcept =
            await Assert.ThrowsAsync<EntitiesValidationsException>(async () => await useCases.Push(userList));

        Assert.Equal(2, actualExcept.Errors.Length);
        Assert.True(actualExcept.Errors[0].Errors.ContainsKey("Username"));
        Assert.True(actualExcept.Errors[1].Errors.ContainsKey("Username"));
    }

    [Fact]
    public async Task Push_HandlingRadiusAttributeFactoryException_ThrowEntitiesValidationsException()
    {
        var radiusAttrFactMock = new Mock<IRadiusAttributeFactory>();
        radiusAttrFactMock.Setup(factory => factory.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new EntityValidationException(new Dictionary<string, object>() { ["attr"] = "fail" }
                .ToImmutableDictionary()));

        var useCases = new UserUseCases(new Mock<IUserFactory>().Object, radiusAttrFactMock.Object,
            new Mock<IUserGroupFactory>().Object, new Mock<IUserRepository>().Object,
            new Mock<IGroupRepository>().Object);

        var userList = new List<UserPushInDto>()
        {
            new("user1", new List<AttributeDto>()
            {
                new("Attr1", "", ""),
                new("Attr2", "", ""),
            }, new List<UserGroupDto>())
        };

        var actualExcept =
            await Assert.ThrowsAsync<EntitiesValidationsException>(async () => await useCases.Push(userList));

        Assert.Single(actualExcept.Errors);
        Assert.True(actualExcept.Errors[0].Errors.ContainsKey("Attributes"));

        var actualAttributesExcept = actualExcept.Errors[0].Errors["Attributes"];

        Assert.Equal(new EntityValidationException[1].GetType(), actualAttributesExcept.GetType());
        Assert.Equal(2, ((EntityValidationException[])actualAttributesExcept).Length);
    }

    [Fact]
    public async Task Push_GroupNotFound_ThrowEntitiesValidationsException()
    {
        var groupRepositoryMock = new Mock<IGroupRepository>();
        groupRepositoryMock.Setup(repository => repository.GetByName(It.IsAny<string>()))
            .Returns((Group?)null);

        var useCases = new UserUseCases(new Mock<IUserFactory>().Object, new Mock<IRadiusAttributeFactory>().Object,
            new Mock<IUserGroupFactory>().Object, new Mock<IUserRepository>().Object, groupRepositoryMock.Object);

        var userList = new List<UserPushInDto>()
        {
            new("user1", new List<AttributeDto>(), new List<UserGroupDto>()
            {
                new("Group1", 1),
                new("Group2", 1),
            })
        };

        var actualExcept =
            await Assert.ThrowsAsync<EntitiesValidationsException>(async () => await useCases.Push(userList));

        Assert.Single(actualExcept.Errors);

        var actualEntitiesExcept = actualExcept.Errors[0].Errors["Groups"];
        Assert.Equal(new EntityValidationException[1].GetType(), actualEntitiesExcept.GetType());

        var actualGroupsExcept = (EntityValidationException[])actualEntitiesExcept;
        Assert.Equal(2, actualGroupsExcept.Length);
        Assert.True(actualGroupsExcept[0].Errors.ContainsKey("Key"));
        Assert.True(actualGroupsExcept[1].Errors.ContainsKey("Key"));
    }

    [Fact]
    public async Task Push_HandlingUserGroupFactoryException_ThrowEntitiesValidationsException()
    {
        var userGroupFactMock = new Mock<IUserGroupFactory>();
        userGroupFactMock.Setup(factory => factory.Create(It.IsAny<int>()))
            .Throws(new EntityValidationException(new Dictionary<string, object>() { ["Priority"] = "fail" }
                .ToImmutableDictionary()));

        var groupRepositoryMock = new Mock<IGroupRepository>();
        groupRepositoryMock.Setup(repository => repository.GetByName(It.IsAny<string>()))
            .Returns(new Group());

        var useCases = new UserUseCases(new Mock<IUserFactory>().Object, new Mock<IRadiusAttributeFactory>().Object,
            userGroupFactMock.Object, new Mock<IUserRepository>().Object, groupRepositoryMock.Object);

        var userList = new List<UserPushInDto>()
        {
            new("user1", new List<AttributeDto>(), new List<UserGroupDto>()
            {
                new("Group1", 0),
                new("Group2", 0),
            })
        };

        var actualExcept =
            await Assert.ThrowsAsync<EntitiesValidationsException>(async () => await useCases.Push(userList));

        Assert.Single(actualExcept.Errors);

        var actualEntitiesExcept = actualExcept.Errors[0].Errors["Groups"];
        Assert.Equal(new EntityValidationException[1].GetType(), actualEntitiesExcept.GetType());

        var actualGroupsExcept = (EntityValidationException[])actualEntitiesExcept;
        Assert.Equal(2, actualGroupsExcept.Length);
        Assert.True(actualGroupsExcept[0].Errors.ContainsKey("Priority"));
        Assert.True(actualGroupsExcept[1].Errors.ContainsKey("Priority"));
    }

    
}