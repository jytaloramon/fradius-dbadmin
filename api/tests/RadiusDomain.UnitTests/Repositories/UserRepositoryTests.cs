using Moq;
using RadiusDomain.Repositories;
using RadiusDomain.Repositories.Interfaces;
using RadiusDomain.UnitTests.SGBDs;

namespace RadiusDomain.UnitTests.Repositories;

public class UserRepositoryTests
{
    private readonly UserRepository _userRepository;

    public UserRepositoryTests()
    {
        _userRepository = new UserRepository(PostgresSgbdConnectionUnitTests.GetInstance(),
            new Mock<IRadAttributeRepository>().Object);
    }

    [Fact]
    public async void GetByName_NotFound_ReturnNull()
    {
        var actualUser = await _userRepository.GetByName(Guid.NewGuid().ToString());

        Assert.Null(actualUser);
    }
}