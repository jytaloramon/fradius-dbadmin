using FradminDomain.Entities;
using FradminDomain.Repositories;
using FradminDomain.SGBDs;
using FradminDomain.ValueObjects;

namespace FradminDomain.UnitTest.Repositories;

public class AdminGroupRepositoryUnitTests
{
    private readonly AdminGroupRepository _repository = new AdminGroupRepository(
        new PostgresConnection("localhost", "33000", "dev", "dev", "db_frdbadmin"));

    [Fact]
    public async Task GetByName_NotFound_ReturnNull()
    {
        var actual = await _repository.GetByName(Guid.NewGuid().ToString()[..16])!;

        Assert.Null(actual);
    }

    [Fact]
    public async Task GetByName_Found_ReturnTheEntity()
    {
        var name = Guid.NewGuid().ToString()[..16];

        var newGroup = new AdminGroup { Name = name, Rules = new HashSet<Rules>(new[] { (Rules)1 }) };
        await _repository.Insert(newGroup);

        var actual = await _repository.GetByName(name)!;

        Assert.NotNull(actual);
        Assert.True(actual.Id > 0);
        Assert.Equal(name, actual.Name);
        Assert.NotEmpty(actual.Rules);
    }

    [Fact]
    public async Task Insert_OneAdminGroupWithTwoRoles_Return3()
    {
        var group = new AdminGroup
            { Name = Guid.NewGuid().ToString()[..16], Rules = new HashSet<Rules>(new[] { (Rules)40, (Rules)100 }) };
       
        var actual = await _repository.Insert(group);

        Assert.True(actual.Id > 0);
    }
}