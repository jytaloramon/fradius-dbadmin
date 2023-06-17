using RadiusDomain.Entities;
using RadiusDomain.Repositories;

namespace RadiusDomain.UnitTests.Repositories;

public class RadAttributeRepositoryTests
{
    private static RadAttributeRepository GetRadAttributeRepository()
    {
        return new RadAttributeRepository(new[]
        {
            new RadGroup("group1", new[] { "attr1", "attr2" }),
            new RadGroup("group2", new[] { "attr3" }),
        });
    }

    [Fact]
    public void Constructor_DuplicateGroup_ThrowException()
    {
        var radGroups = new[]
        {
            new RadGroup("group1", new[] { "attr1" }),
            new RadGroup("group2", new[] { "attr2" }),
            new RadGroup("group1", new[] { "attr3" }),
        };

        var actualExcept = Assert.Throws<Exception>(() => new RadAttributeRepository(radGroups));

        Assert.Equal("Duplicate Group, code (group1).", actualExcept.Message);
    }

    [Fact]
    public void Constructor_DuplicateAttribute_ThrowException()
    {
        var radGroups = new[]
        {
            new RadGroup("group1", new[] { "attr1" }),
            new RadGroup("group2", new[] { "attr1" }),
        };

        var actualExcept = Assert.Throws<Exception>(() => new RadAttributeRepository(radGroups));

        Assert.Equal("Duplicate Attribute (attr1) in the groups \"group1\" and \"group2\"", actualExcept.Message);
    }

    [Fact]
    public void GetGroupCodeByAttribute_NotFound_ReturnNull()
    {
        var repository = GetRadAttributeRepository();

        var actualGroupCode = repository.GetGroupCodeByAttribute("");

        Assert.Null(actualGroupCode);
    }

    [Fact]
    public void GetGroupCodeByAttribute_Found_ReturnAStringValue()
    {
        var repository = GetRadAttributeRepository();

        var actualGroupCode = repository.GetGroupCodeByAttribute("attr1");

        Assert.NotNull(actualGroupCode);
        Assert.Equal("group1", actualGroupCode);
    }

    [Fact]
    public void GetRadGroupByCode_NotFound_ReturnNull()
    {
        var repository = GetRadAttributeRepository();

        var actualGroup = repository.GetRadGroupByCode("");

        Assert.Null(actualGroup);
    }

    [Fact]
    public void GetRadGroupByCode_Found_ReturnAStringValue()
    {
        var repository = GetRadAttributeRepository();

        const string expectedCode = "group1";

        var actualGroup = repository.GetRadGroupByCode(expectedCode);

        Assert.NotNull(actualGroup);
        Assert.Equal(expectedCode, actualGroup.Code);
    }

    [Fact]
    public void GetAllGroups_ReturnAArrayWithLengthEqual2()
    {
        var repository = GetRadAttributeRepository();

        var actualGroups = repository.GetAllGroups();

        Assert.Equal(2, actualGroups.Length);
    }
}