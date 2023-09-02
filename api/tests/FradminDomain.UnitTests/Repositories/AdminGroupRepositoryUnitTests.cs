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
    public async Task GetByName_NotFound_ReturnNull()
    {
        var actual = await _repository.GetByName(Guid.NewGuid().ToString()[..16])!;

        Assert.Null(actual);
    }

    [Fact]
    public async Task GetByName_Found_ReturnTheEntity()
    {
        var newGroup = new AdminGroup { Name = Guid.NewGuid().ToString()[..16], Rules = new HashSet<Rules>(new[] { (Rules)1 }) };
        await _repository.Insert(newGroup);

        var actual = await _repository.GetByName(newGroup.Name)!;

        Assert.NotNull(actual);
        Assert.True(actual.Id > 0);
        Assert.Equal(newGroup.Name, actual.Name);
        Assert.NotEmpty(actual.Rules);
    }
    
    [Fact]
    public async Task GetAll_InsertAdminGroup_ReturnAListGreaterThanOrEqual1(){
        var rulesArray = new[] { (Rules)1000, (Rules)2000 };
        var newGroup = new AdminGroup { Name = Guid.NewGuid().ToString()[..16], Rules = new HashSet<Rules>(rulesArray) };
        await _repository.Insert(newGroup);

        var allGroups = await _repository.GetAll();
        
        Assert.True(allGroups.Count > 1);

        var groupCreated = allGroups.First(group => group.Name.Equals(newGroup.Name));
        
        Assert.True(groupCreated.Rules.SetEquals(rulesArray));
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