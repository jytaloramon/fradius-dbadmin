using Moq;
using RadiusDomain.Entities;
using RadiusDomain.Repositories;
using RadiusDomain.Repositories.Interfaces;
using RadiusDomain.SGBDs.Interfaces;
using RadiusDomain.UnitTests.SGBDs;

namespace RadiusDomain.UnitTests.Repositories;

public class GroupRepositoryTests
{
    private readonly ISgbd _sgbd;

    public GroupRepositoryTests()
    {
        _sgbd = PostgresSgbdConnectionUnitTests.GetInstance();
    }

    private static IRadAttributeMerge GetRadAttributeMerge(List<RadiusAttribute>? toRemove,
        List<RadiusAttribute>? toUpdate, List<RadiusAttribute>? toInsert)
    {
        var radAttrMergeMock = new Mock<IRadAttributeMerge>();
        radAttrMergeMock.Setup(attrMerge =>
                attrMerge.Merge(It.IsAny<List<RadiusAttribute>>(), It.IsAny<List<RadiusAttribute>>()))
            .Returns(new RadAttributeDbOperation(toRemove, toUpdate, toInsert));

        return radAttrMergeMock.Object;
    }

    [Fact]
    public async Task Insert_OnlyToInsertAttrs_ReturnTheValue2()
    {
        var attrMerge = GetRadAttributeMerge(null, null, new List<RadiusAttribute>
        {
            new() { Name = "Attr1", Op = ":=", Value = "Value1" },
            new() { Name = "Attr2", Op = ":=", Value = "Value2" }
        });

        var repository = new GroupRepository(_sgbd, attrMerge);
        var actualRowAffected = await repository.Insert(new Group
            { Name = Guid.NewGuid().ToString(), Attributes = new List<RadiusAttribute>() });

        Assert.Equal(2, actualRowAffected);
    }

    [Fact]
    public async Task Insert_OnlyToUpdateAttrs_ReturnAListUpdated()
    {
        var groupName = Guid.NewGuid().ToString();

        var beforeUpAttrMerge = GetRadAttributeMerge(null, null, new List<RadiusAttribute>
        {
            new() { Name = "Attr1", Op = ":=", Value = "Value1" },
            new() { Name = "Attr2", Op = ":=", Value = "Value2" }
        });

        var beforeUpRepository = new GroupRepository(_sgbd, beforeUpAttrMerge);
        await beforeUpRepository.Insert(new Group { Name = groupName, Attributes = new List<RadiusAttribute>() });
        var groupBeforeUp = await beforeUpRepository.GetByKey(groupName);

        var attrMerge = GetRadAttributeMerge(null, new List<RadiusAttribute>
        {
            new()
            {
                Id = groupBeforeUp!.Attributes![0].Id, Name = $"{groupBeforeUp.Attributes![0].Name}Up", Op = "==",
                Value = $"{groupBeforeUp.Attributes![0].Value}Up"
            },
            new()
            {
                Id = groupBeforeUp.Attributes![1].Id, Name = $"{groupBeforeUp.Attributes![1].Name}Up", Op = "==",
                Value = $"{groupBeforeUp.Attributes![1].Value}Up"
            }
        }, null);

        var repository = new GroupRepository(_sgbd, attrMerge);
        var actualRowAffected = await repository.Insert(new Group
            { Name = groupName, Attributes = new List<RadiusAttribute>() });
        var actualGroup = await repository.GetByKey(groupName);

        Assert.Equal(2, actualRowAffected);

        var expectedAttrsText = groupBeforeUp.Attributes!.Select(
            attr => $"{attr.Id},{attr.Name}Up,==,{attr.Value}Up");
        var actualAttrText = actualGroup!.Attributes!.Select(
            attr => $"{attr.Id},{attr.Name},{attr.Op},{attr.Value}");

        Assert.True(new HashSet<string>(expectedAttrsText).SetEquals(actualAttrText));
    }

    [Fact]
    public async Task Insert_RemoveAllAndAddNewAttrs_ReturnAListUpdated()
    {
        var groupName = Guid.NewGuid().ToString();

        var beforeUpAttrMerge = GetRadAttributeMerge(null, null, new List<RadiusAttribute>
        {
            new() { Name = "Attr1", Op = ":=", Value = "Value1" },
            new() { Name = "Attr2", Op = ":=", Value = "Value2" }
        });

        var beforeUpRepository = new GroupRepository(_sgbd, beforeUpAttrMerge);
        var beforeRowAffected = await beforeUpRepository.Insert(new Group
            { Name = groupName, Attributes = new List<RadiusAttribute>() });

        Assert.Equal(2, beforeRowAffected);

        var groupBeforeUp = await beforeUpRepository.GetByKey(groupName);
        var attrMerge = GetRadAttributeMerge(groupBeforeUp!.Attributes, null, new List<RadiusAttribute>
        {
            new() { Name = "Attr3", Op = ":=", Value = "Value3" },
            new() { Name = "Attr4", Op = ":=", Value = "Value4" },
            new() { Name = "Attr5", Op = ":=", Value = "Value5" }
        });

        var repository = new GroupRepository(_sgbd, attrMerge);
        var actualRowAffected = await repository.Insert(new Group
            { Name = groupName, Attributes = new List<RadiusAttribute>() });

        Assert.Equal(5, actualRowAffected);

        var actualGroup = await repository.GetByKey(groupName);
        var actualAttrsIds = new HashSet<int>(actualGroup!.Attributes!.Select(attr => attr.Id));

        Assert.Equal(3, actualAttrsIds.Count);
        Assert.DoesNotContain(groupBeforeUp.Attributes![0].Id, actualAttrsIds);
        Assert.DoesNotContain(groupBeforeUp.Attributes![1].Id, actualAttrsIds);
    }

    [Fact]
    public async Task Insert_MixToRemoveUpdateAndInsert_ReturnAListUpdated()
    {
        var groupName = Guid.NewGuid().ToString();

        var beforeUpAttrMerge = GetRadAttributeMerge(null, null, new List<RadiusAttribute>
        {
            new() { Name = "Attr1", Op = ":=", Value = "Value1" },
            new() { Name = "Attr2", Op = ":=", Value = "Value2" },
            new() { Name = "Attr3", Op = ":=", Value = "Value3" },
        });

        var beforeUpRepository = new GroupRepository(_sgbd, beforeUpAttrMerge);
        var beforeRowAffected = await beforeUpRepository.Insert(new Group
            { Name = groupName, Attributes = new List<RadiusAttribute>() });

        Assert.Equal(3, beforeRowAffected);

        var groupBeforeUp = await beforeUpRepository.GetByKey(groupName);
        var toRemove = groupBeforeUp!.Attributes!.GetRange(0, 1);
        var toUpdate = groupBeforeUp.Attributes.GetRange(1, 2);

        var attrMerge = GetRadAttributeMerge(toRemove,
            toUpdate.Select(attr => new RadiusAttribute
            {
                Id = attr.Id, Name = $"{attr.Name}Up", Op = "==", Value = $"{attr.Value}Up"
            }).ToList(),
            new List<RadiusAttribute>
            {
                new() { Name = "Attr4", Op = ":=", Value = "Value4" },
                new() { Name = "Attr5", Op = ":=", Value = "Value5" },
                new() { Name = "Attr6", Op = ":=", Value = "Value6" },
            });

        var repository = new GroupRepository(_sgbd, attrMerge);
        var actualRowAffected = await repository.Insert(new Group
            { Name = groupName, Attributes = new List<RadiusAttribute>() });

        Assert.Equal(6, actualRowAffected);

        var actualGroup = await repository.GetByKey(groupName);
        Assert.Equal(5, actualGroup!.Attributes!.Count);
        
        
    }

    [Fact]
    public async Task GetByKey_NotFound_ReturnNull()
    {
        var repository = new GroupRepository(_sgbd, new Mock<IRadAttributeMerge>().Object);
        var actualGroup = await repository.GetByKey("");

        Assert.Null(actualGroup);
    }

    [Fact]
    public async Task GetByKey_Found_ReturnAnGroup()
    {
        var attrMerge = GetRadAttributeMerge(null, null, new List<RadiusAttribute>
        {
            new() { Name = "Attr1", Op = ":=", Value = "Value1" },
            new() { Name = "Attr2", Op = ":=", Value = "Value2" }
        });

        var repository = new GroupRepository(_sgbd, attrMerge);

        var groupName = Guid.NewGuid().ToString();
        await repository.Insert(new Group { Name = groupName, Attributes = new List<RadiusAttribute>() });

        var actualGroup = await repository.GetByKey(groupName);

        Assert.NotNull(actualGroup);
        Assert.Equal(2, actualGroup.Attributes!.Count);
    }
}