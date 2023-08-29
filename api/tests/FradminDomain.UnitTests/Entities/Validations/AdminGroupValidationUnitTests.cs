using FradminDomain.Entities;
using FradminDomain.Entities.Validations;
using FradminDomain.ValueObjects;

namespace FradminDomain.UnitTest.Entities.Validations;

public class AdminGroupValidationUnitTests : BaseUnitTestsEntityValidation<AdminGroup>
{
    public AdminGroupValidationUnitTests() : base(new AdminGroupValidation())
    {
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-(1 << 15))]
    public void PropertyId_LessThanOrEqualTo0_ReturnInvalidResult(short id)
    {
        const string property = "Id";
        var adminGroup = new AdminGroup { Id = id };
        var actual = RunValidator(adminGroup, property);

        Assert.False(actual.IsValid);
        Assert.Single(actual.Errors);
        Assert.Equal(property, actual.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.OutOfRangeMin.Code, actual.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(1)]
    [InlineData((1 << 15) - 1)]
    public void PropertyId_GreaterThan0_ReturnValidResult(short id)
    {
        var adminGroup = new AdminGroup { Id = id };
        var actual = RunValidator(adminGroup, "Id");

        Assert.True(actual.IsValid);
    }

    [Fact]
    public void PropertyName_Empty_ReturnInvalidResult()
    {
        const string property = "Name";
        var adminGroup = new AdminGroup { Name = "" };
        var actual = RunValidator(adminGroup, property);

        Assert.False(actual.IsValid);
        Assert.Single(actual.Errors);
        Assert.Equal(property, actual.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.Required.Code, actual.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(33)]
    [InlineData(64)]
    [InlineData(1024)]
    public void PropertyName_GreaterThan32_ReturnInvalidResult(int lengthName)
    {
        const string property = "Name";
        var adminGroup = new AdminGroup { Name = new string('a', lengthName) };
        var actual = RunValidator(adminGroup, property);

        Assert.False(actual.IsValid);
        Assert.Single(actual.Errors);
        Assert.Equal(property, actual.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.OutOfRangeMax.Code, actual.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(16)]
    [InlineData(32)]
    public void PropertyName_NoEmptyAndLessThanOrEqualTo32_ReturnValidResult(int lengthName)
    {
        var adminGroup = new AdminGroup { Name = new string('a', lengthName) };
        var actual = RunValidator(adminGroup, "Name");

        Assert.True(actual.IsValid);
    }

    [Fact]
    public void PropertyRules_Empty_ReturnInvalidResult()
    {
        const string property = "Rules";
        var adminGroup = new AdminGroup { Rules = new HashSet<Rules>() };
        var actual = RunValidator(adminGroup, property);

        Assert.False(actual.IsValid);
        Assert.Single(actual.Errors);
        Assert.Equal(property, actual.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.Required.Code, actual.Errors[0].ErrorCode);
    }

    [Fact]
    public void PropertyRules_WithNonExistentValues_ReturnInvalidResult()
    {
        const string property = "Rules";
        const int multi = 1024;

        var adminGroup = new AdminGroup
        {
            Rules = new HashSet<Rules>(new[]
            {
                Rules.ManagementAdmin,
                (Rules)(multi * 4),
                Rules.ManagementGroup,
                (Rules)(multi * 8),
            })
        };
        var actual = RunValidator(adminGroup, property);

        Assert.False(actual.IsValid);
        Assert.Equal(2, actual.Errors.Count);
        Assert.Equal(new[] { $"{property}[1]", $"{property}[3]" },
            new[] { actual.Errors[0].PropertyName, actual.Errors[1].PropertyName });
        Assert.Equal(new[] { ErrorCatalog.NotFound.Code, ErrorCatalog.NotFound.Code },
            new[] { actual.Errors[0].ErrorCode, actual.Errors[1].ErrorCode });
    }

    [Fact]
    public void PropertyRules_OnlyExistentValues_ReturnValidResult()
    {
        var adminGroup = new AdminGroup
        {
            Rules = new HashSet<Rules>(new[]
            {
                Rules.ManagementAdmin,
                Rules.ManagementGroup
            })
        };
        var actual = RunValidator(adminGroup, "Rules");

        Assert.True(actual.IsValid);
    }
}