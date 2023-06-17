using Moq;
using RadiusDomain.Entities;
using RadiusDomain.Repositories;
using RadiusDomain.Repositories.Interfaces;
using RadiusDomain.SGBDs.Psql;
using RadiusDomain.UnitTests.SGBDs;

namespace RadiusDomain.UnitTests.Repositories;

public class UserRepositoryTests
{
    private static UserRepository CreateUserRepository(IRadAttributeRepository radAttributeRepository)
    {
        return new UserRepository(PostgresSgbdConnectionUnitTests.GetInstance(), radAttributeRepository);
    }

    private static List<RadiusAttribute> GetDefaultRadiusAttributeList()
    {
        return new List<RadiusAttribute>()
        {
            new RadiusAttribute { Name = "Attr1", Op = ":=", Value = "Value1" },
            new RadiusAttribute { Name = "Attr2", Op = ":=", Value = "Value2" }
        };
    }

    [Fact]
    public async Task InsertMany_HandlingGetByNameException_ThrowArgumentException()
    {
        var user = new User { Username = Guid.NewGuid().ToString(), Attributes = GetDefaultRadiusAttributeList() };

        var userRepository = new UserRepository(new PostgresConnection("", "", "", ""),
            new Mock<IRadAttributeRepository>().Object);

        await Assert.ThrowsAsync<ArgumentException>(() => userRepository.InsertMany(new List<User>() { user }));
    }

    [Fact]
    public async Task InsertMany_MultiUsers_ShouldNotHaveErrors()
    {
        var user1 = new User { Username = Guid.NewGuid().ToString(), Attributes = GetDefaultRadiusAttributeList() };
        var user2 = new User { Username = Guid.NewGuid().ToString(), Attributes = GetDefaultRadiusAttributeList() };

        var userRepository = CreateUserRepository(new Mock<IRadAttributeRepository>().Object);
        await userRepository.InsertMany(new List<User>() { user1, user2 });

        var actualUser1 = await userRepository.GetByName(user1.Username);
        Assert.NotNull(actualUser1);

        var actualUser2 = await userRepository.GetByName(user2.Username);
        Assert.NotNull(actualUser2);
    }

    [Fact]
    public async Task InsertMany_UpdateAttrData_UpdateWithoutChangingId()
    {
        var userBeforeUpRepository = CreateUserRepository(new Mock<IRadAttributeRepository>().Object);

        var userBeforeUp = new User
            { Username = Guid.NewGuid().ToString(), Attributes = GetDefaultRadiusAttributeList() };
        await userBeforeUpRepository.InsertMany(new List<User>() { userBeforeUp });
        var actualUserBeforeUp = await userBeforeUpRepository.GetByName(userBeforeUp.Username);

        var userUp = new User
        {
            Username = userBeforeUp.Username, Attributes = new List<RadiusAttribute>()
            {
                new RadiusAttribute { Name = "Attr1", Op = "==", Value = "Value1new" },
                new RadiusAttribute { Name = "Attr4", Op = ":=", Value = "Value4" }
            }
        };

        var radAttrRepositoryMock = new Mock<IRadAttributeRepository>();
        radAttrRepositoryMock.Setup(repo => repo.GetGroupCodeByAttribute(It.IsAny<string>()))
            .Returns<string>((attribute) => attribute.Equals("Attr1") ? "a1b2c" : Guid.NewGuid().ToString()[0..4]);

        var userUpRepository = CreateUserRepository(radAttrRepositoryMock.Object);
        await userUpRepository.InsertMany(new List<User>() { userUp });
        var actualUserUp = await userUpRepository.GetByName(userUp.Username);

        Assert.NotNull(actualUserUp);
        Assert.Equal(3, actualUserUp.Attributes.Count);

        var attr1BeforeUp = actualUserBeforeUp!.Attributes.First(attr => attr.Name.Equals("Attr1"));
        var attr1Up = actualUserUp.Attributes.First(attr => attr.Name.Equals("Attr1"));

        Assert.Equal(attr1BeforeUp.Id, attr1Up.Id);
        Assert.Equal(attr1BeforeUp.Name, attr1Up.Name);
        Assert.NotEqual(attr1BeforeUp.Op, attr1Up.Op);
        Assert.NotEqual(attr1BeforeUp.Value, attr1Up.Value);
    }

    [Fact]
    public async Task InsertMany_ReplaceAttrAnotherFromSameGroup_ShouldNotHaveErrors()
    {
        var userBeforeUpRepository = CreateUserRepository(new Mock<IRadAttributeRepository>().Object);

        var userBeforeUp = new User
            { Username = Guid.NewGuid().ToString(), Attributes = GetDefaultRadiusAttributeList() };
        await userBeforeUpRepository.InsertMany(new List<User>() { userBeforeUp });
        var actualUserBeforeUp = await userBeforeUpRepository.GetByName(userBeforeUp.Username);

        var userUp = new User
        {
            Username = userBeforeUp.Username, Attributes = new List<RadiusAttribute>()
            {
                new RadiusAttribute { Name = "Attr3", Op = "==", Value = "Value3" },
                new RadiusAttribute { Name = "Attr4", Op = ":=", Value = "Value4" }
            }
        };

        var radAttrRepositoryMock = new Mock<IRadAttributeRepository>();
        radAttrRepositoryMock.Setup(repo => repo.GetGroupCodeByAttribute(It.IsAny<string>()))
            .Returns<string>((attribute) => attribute.Equals("Attr1") || attribute.Equals("Attr3")
                ? "a1b2c"
                : Guid.NewGuid().ToString()[0..4]);

        var userUpRepository = CreateUserRepository(radAttrRepositoryMock.Object);
        await userUpRepository.InsertMany(new List<User>() { userUp });
        var actualUserUp = await userUpRepository.GetByName(userUp.Username);

        Assert.NotNull(actualUserUp);
        Assert.Equal(3, actualUserUp.Attributes.Count);

        var attr1Up = actualUserUp.Attributes.Find(attr => attr.Name.Equals("Attr1"));
        Assert.Null(attr1Up);

        var attr1BeforeUp = actualUserBeforeUp!.Attributes.First(attr => attr.Name.Equals("Attr1"));
        var attr3Up = actualUserUp.Attributes.First(attr => attr.Name.Equals("Attr3"));

        Assert.NotEqual(attr1BeforeUp.Id, attr3Up.Id);
    }

    [Fact]
    public async Task GetByName_NotFound_ReturnNull()
    {
        var userRepository = CreateUserRepository(new Mock<IRadAttributeRepository>().Object);
        var actualUser = await userRepository.GetByName("");

        Assert.Null(actualUser);
    }

    [Fact]
    public async Task GetByName_Found_ReturnUser()
    {
        var expectedUsername = Guid.NewGuid().ToString();
        var user1 = new User { Username = expectedUsername, Attributes = GetDefaultRadiusAttributeList() };

        var userRepository = CreateUserRepository(new Mock<IRadAttributeRepository>().Object);
        await userRepository.InsertMany(new List<User>() { user1 });

        var actualUser = await userRepository.GetByName(expectedUsername);

        Assert.NotNull(actualUser);
        Assert.Equal(expectedUsername, actualUser.Username);
    }

    [Fact]
    public async Task GetAll_ReturnAListGreaterThan0()
    {
        var user = new User { Username = Guid.NewGuid().ToString(), Attributes = GetDefaultRadiusAttributeList() };

        var userRepository = CreateUserRepository(new Mock<IRadAttributeRepository>().Object);
        await userRepository.InsertMany(new List<User>() { user });

        var actualUsers = await userRepository.GetAll();

        Assert.True(actualUsers.Count > 0);
    }
}