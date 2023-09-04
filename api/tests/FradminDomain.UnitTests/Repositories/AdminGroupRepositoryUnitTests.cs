using FradminDomain.Entities;
using FradminDomain.Repositories;
using FradminDomain.SGBDs;
using FradminDomain.ValueObjects;

namespace FradminDomain.UnitTest.Repositories;

public class AdminGroupRepositoryUnitTests
{
    private readonly AdminGroupRepository _repository = new AdminGroupRepository(
        new PostgresConnection("psqldb", "5432", "dev", "dev", "db_frdbadmin"));

    [Fact]
    public async Task GetById_NotFound_ReturnNull()
    {
        var actual = await _repository.GetById(0);

        Assert.Null(actual);
    }

    [Fact]
    public async Task GetById_Found_ReturnTheEntity()
    {
        var group = new AdminGroup
            { Name = Guid.NewGuid().ToString()[..16], Rules = new HashSet<Rules>(new[] { (Rules)1 }) };

        await _repository.Save(group);
        var idFind = (await _repository.GetByName(group.Name))!.Id;
        var groupFound = await _repository.GetById(idFind);

        Assert.NotNull(groupFound);
        Assert.True(groupFound.Id > 0);
        Assert.Equal(group.Name, groupFound.Name);
        Assert.True(group.Rules.SetEquals(groupFound.Rules));
    }

    [Fact]
    public async Task GetByName_NotFound_ReturnNull()
    {
        var actual = await _repository.GetByName(Guid.NewGuid().ToString()[..16]);

        Assert.Null(actual);
    }

    [Fact]
    public async Task GetByName_Found_ReturnTheEntity()
    {
        var group = new AdminGroup
            { Name = Guid.NewGuid().ToString()[..16], Rules = new HashSet<Rules>(new[] { (Rules)1 }) };

        await _repository.Save(group);
        var groupFound = await _repository.GetByName(group.Name);

        Assert.NotNull(groupFound);
        Assert.True(groupFound.Id > 0);
        Assert.Equal(group.Name, groupFound.Name);
        Assert.True(group.Rules.SetEquals(groupFound.Rules));
    }

    [Fact]
    public async Task GetAll_InsertAdminGroup_ReturnAListGreaterThanOrEqual1()
    {
        var group = new AdminGroup
            { Name = Guid.NewGuid().ToString()[..16], Rules = new HashSet<Rules>(new[] { (Rules)1000, (Rules)2000 }) };

        await _repository.Save(group);
        var allGroups = await _repository.GetAll();

        Assert.NotEmpty(allGroups);
    }

    [Fact]
    public async Task Save_AdminGroupWithTwoRoles_ReturnValueGreaterThan0()
    {
        var group = new AdminGroup
            { Name = Guid.NewGuid().ToString()[..16], Rules = new HashSet<Rules>(new[] { (Rules)40, (Rules)100 }) };

        var affectedRow = await _repository.Save(group);

        Assert.True(affectedRow > 0);
    }
}