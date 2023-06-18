using Moq;
using RadiusDomain.Entities;
using RadiusDomain.Repositories;
using RadiusDomain.Repositories.Interfaces;

namespace RadiusDomain.UnitTests.Repositories;

public class RadAttributeMergeTests
{
    private readonly RadAttributeMerge _radAttributeMerge;

    public RadAttributeMergeTests()
    {
        var radAttrRepositoryMock = new Mock<IRadAttributeRepository>();
        radAttrRepositoryMock.Setup(repo => repo.GetGroupCodeByAttribute(It.IsAny<string>()))
            .Returns<string>((attr) =>
            {
                switch (attr)
                {
                    case "Attr1":
                    case "Attr3":
                        return "a1b2c";
                    case "Attr2":
                    case "Attr4":
                        return "d4e5f";
                    default:
                        return Guid.NewGuid().ToString()[0..4];
                }
            });

        _radAttributeMerge = new RadAttributeMerge(radAttrRepositoryMock.Object);
    }

    private static IComparer<RadiusAttribute> GetComparerAttrByName()
    {
        var comparerMock = new Mock<IComparer<RadiusAttribute>>();
        comparerMock.Setup(comp => comp.Compare(It.IsAny<RadiusAttribute>(), It.IsAny<RadiusAttribute>()))
            .Returns<RadiusAttribute?, RadiusAttribute?>((x, y) =>
                string.Compare(x!.Name, y!.Name, StringComparison.Ordinal));

        return comparerMock.Object;
    }

    [Fact]
    public void Merge_EmptyCurrentAttrs_OnlyToInsertMustHaveAttrs()
    {
        var newAttrs = new List<RadiusAttribute>
        {
            new() { Name = "Attr1", Op = ":=", Value = "Value1" },
            new() { Name = "Attr2", Op = ":=", Value = "Value2" },
        };

        var actualDbOperation = _radAttributeMerge.Merge(new List<RadiusAttribute>(), newAttrs);

        Assert.Null(actualDbOperation.ToRemove);
        Assert.Null(actualDbOperation.ToUpdate);
        Assert.NotNull(actualDbOperation.ToInsert);
        Assert.True(new HashSet<RadiusAttribute>(actualDbOperation.ToInsert).SetEquals(newAttrs));
    }

    [Fact]
    public void Merge_UpdateAttributeData_OnlyToUpdateMustHaveAttrs()
    {
        var currentAttrs = new List<RadiusAttribute>
        {
            new() { Id = 1, Name = "Attr1", Op = ":=", Value = "Value1" },
            new() { Id = 2, Name = "Attr2", Op = ":=", Value = "Value2" },
        };

        var newAttrs = new List<RadiusAttribute>
        {
            new() { Name = "Attr1", Op = "==", Value = "Value1new" },
            new() { Name = "Attr2", Op = "==", Value = "Value2new" },
        };

        var actualDbOperation = _radAttributeMerge.Merge(currentAttrs, newAttrs);

        Assert.Null(actualDbOperation.ToRemove);
        Assert.NotNull(actualDbOperation.ToUpdate);
        Assert.Null(actualDbOperation.ToInsert);
        Assert.True(new HashSet<RadiusAttribute>(actualDbOperation.ToUpdate).SetEquals(newAttrs));

        actualDbOperation.ToUpdate.Sort(GetComparerAttrByName());
        Assert.Equal(new[] { 1, 2 }, new[] { newAttrs[0].Id, newAttrs[1].Id });
    }

    [Fact]
    public void Merge_ReplaceAttrsWithOthersFromTheSameGroup_CurrentAreRemovedAndNewAreInserted()
    {
        var currentAttrs = new List<RadiusAttribute>
        {
            new() { Id = 1, Name = "Attr1", Op = ":=", Value = "Value1" },
            new() { Id = 2, Name = "Attr2", Op = ":=", Value = "Value2" },
        };

        var newAttrs = new List<RadiusAttribute>
        {
            new() { Name = "Attr3", Op = ":=", Value = "Value3" },
            new() { Name = "Attr4", Op = ":=", Value = "Value4" },
        };

        var actualDbOperation = _radAttributeMerge.Merge(currentAttrs, newAttrs);

        Assert.NotNull(actualDbOperation.ToRemove);
        Assert.Null(actualDbOperation.ToUpdate);
        Assert.NotNull(actualDbOperation.ToInsert);
        Assert.True(new HashSet<RadiusAttribute>(actualDbOperation.ToRemove).SetEquals(currentAttrs));
        Assert.True(new HashSet<RadiusAttribute>(actualDbOperation.ToInsert).SetEquals(newAttrs));
    }

    [Fact]
    public void Merge_AnOldAttrDoesNotMatchWithNewAttr_UnmatchedAttrIsRemoved()
    {
        var currentAttrs = new List<RadiusAttribute>
        {
            new() { Id = 1, Name = "Attr1", Op = ":=", Value = "Value1" },
            new() { Id = 2, Name = "Attr2", Op = ":=", Value = "Value2" },
        };

        var newAttrs = new List<RadiusAttribute>
        {
            new() { Id = 1, Name = "Attr1", Op = "==", Value = "Value1New" },
            new() { Name = "Attr5", Op = ":=", Value = "Value5" },
            new() { Name = "Attr6", Op = ":=", Value = "Value6" },
        };

        var actualDbOperation = _radAttributeMerge.Merge(currentAttrs, newAttrs);

        Assert.Single(actualDbOperation.ToRemove!);
        Assert.Equal(2, actualDbOperation.ToRemove![0].Id);
        Assert.Single(actualDbOperation.ToUpdate!);
        Assert.Equal(1, actualDbOperation.ToUpdate![0].Id);
        Assert.Equal(2, actualDbOperation.ToInsert!.Count);

        actualDbOperation.ToInsert.Sort(GetComparerAttrByName());
        Assert.Equal(new[] { "Attr5", "Attr6" },
            new[] { actualDbOperation.ToInsert[0].Name, actualDbOperation.ToInsert[1].Name });
    }
}