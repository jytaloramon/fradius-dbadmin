using RadiusDomain.Entities;
using RadiusDomain.Validations;
using RadiusDomain.ValueObjects;

namespace RadiusDomain.UnitTests.Validations;

public class GroupValidationTests : BaseEntityValidationTests<Group>
{
    public GroupValidationTests() : base(new GroupValidation())
    {
    }

    [Fact]
    public void Name_Empty_Invalid()
    {
        const string property = "Name";
        var actualResult = RunValidator(new Group { Name = "" }, property);

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(property, actualResult.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.RequiredProperty.Code, actualResult.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(65)]
    [InlineData(80)]
    public void Name_GreaterThan64_Invalid(int length)
    {
        const string property = "Name";
        var actualResult = RunValidator(new Group() { Name = new string('*', length) }, property);

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(property, actualResult.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.FieldExceeded.Code, actualResult.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(64)]
    public void Name_GreaterThan0AndLessThanOrEquals64_Valid(int length)
    {
        const string property = "Name";
        var actualResult = RunValidator(new Group() { Name = new string('*', length) }, property);

        Assert.True(actualResult.IsValid);
    }

    [Fact]
    public void Attributes_EmptyList_Invalid()
    {
        const string property = "Attributes";
        var actualResult = RunValidator(new Group(), property);

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(property, actualResult.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.RequiredProperty.Code, actualResult.Errors[0].ErrorCode);
    }

    [Fact]
    public void Attributes_NotEmptyList_Valid()
    {
        const string property = "Attributes";
        var actualResult = RunValidator(new Group() { Attributes = { new RadiusAttribute() } }, property);

        Assert.True(actualResult.IsValid);
    }
}