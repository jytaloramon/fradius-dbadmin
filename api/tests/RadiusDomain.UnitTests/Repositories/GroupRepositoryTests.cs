using RadiusDomain.Entities;
using RadiusDomain.Repositories;
using RadiusDomain.UnitTests.SGBDs;

namespace RadiusDomain.UnitTests.Repositories;

public class GroupRepositoryTests
{
    private readonly GroupRepository _groupRepository;

    public GroupRepositoryTests()
    {
        _groupRepository = new GroupRepository(PostgresSgbdConnectionUnitTests.GetInstance());
    }

    [Fact]
    public async Task Insert_OneAttributeWithError_ReturnNull()
    {
        var groupName = Guid.NewGuid().ToString();

        var group = new Group
        {
            Name = groupName, Attributes = new List<RadiusAttribute>()
            {
                new RadiusAttribute { Name = "Attr1", Op = ":=", Value = "Value1" },
                new RadiusAttribute { Name = "Attr2", Op = ":===", Value = "Value2" }
            }
        };

        await Assert.ThrowsAnyAsync<Exception>(async () => await _groupRepository.Insert(group));

        var actualGroup = await _groupRepository.GetByName(groupName);

        Assert.Null(actualGroup);
    }

    [Fact]
    public async void Insert_WithValidAttrs_ReturnTheValue2()
    {
        var group = new Group
        {
            Name = Guid.NewGuid().ToString(), Attributes = new List<RadiusAttribute>()
            {
                new RadiusAttribute { Name = "Attr1", Op = ":=", Value = "Value1" },
                new RadiusAttribute { Name = "Attr2", Op = ":=", Value = "Value2" }
            }
        };

        var actualRowAffected = await _groupRepository.Insert(group);

        Assert.Equal(2, actualRowAffected);
    }

    [Fact]
    public async void GetByName_NotFound_ReturnNull()
    {
        var actualGroup = await _groupRepository.GetByName(Guid.NewGuid().ToString());

        Assert.Null(actualGroup);
    }

    [Fact]
    public async void GetByName_Found_ReturnAGroup()
    {
        var groupName = Guid.NewGuid().ToString();

        var groupToInsert = new Group
        {
            Name = groupName,
            Attributes = new List<RadiusAttribute>()
            {
                new RadiusAttribute { Name = "Attr1", Op = ":=", Value = "Value1" },
                new RadiusAttribute { Name = "Attr2", Op = ":=", Value = "Value2" }
            }
        };

        await _groupRepository.Insert(groupToInsert);

        var actualGroup = await _groupRepository.GetByName(groupName);

        Assert.NotNull(actualGroup);
        Assert.Equal(groupName, actualGroup.Name);
        Assert.Equal(2, actualGroup.Attributes.Count);
    }

    [Fact]
    public async void GetAll_ReturnAListGreaterThan0()
    {
        var groupToInsert1 = new Group
        {
            Name = Guid.NewGuid().ToString(),
            Attributes = new List<RadiusAttribute>()
                { new RadiusAttribute { Name = "Attr1", Op = ":=", Value = "Value1" } }
        };

        var groupToInsert2 = new Group
        {
            Name = Guid.NewGuid().ToString(),
            Attributes = new List<RadiusAttribute>()
                { new RadiusAttribute { Name = "Attr1", Op = ":=", Value = "Value1" } }
        };

        await _groupRepository.Insert(groupToInsert1);
        await _groupRepository.Insert(groupToInsert2);

        var actualGroups = await _groupRepository.GetAll();

        Assert.True(actualGroups.Count > 0);
    }
}