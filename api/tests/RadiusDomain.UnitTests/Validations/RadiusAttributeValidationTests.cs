using RadiusDomain.Entities;
using RadiusDomain.Validations;
using RadiusDomain.ValueObjects;

namespace RadiusDomain.UnitTests.EntitiesValidation;

public class RadiusAttributeValidationTests : BaseEntityValidationTests<RadiusAttribute>
{
    public RadiusAttributeValidationTests() : base(new RadiusAttributeValidation())
    {
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Id_LessThan1_Invalid(int id)
    {
        const string property = "Id";
        var actualResult = RunValidator(new RadiusAttribute { Id = id }, property);

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(property, actualResult.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.FieldExceeded.Code, actualResult.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(20)]
    [InlineData(128128)]
    public void Id_GreaterThanOrEquals1_Valid(int id)
    {
        const string property = "Id";
        var actualResult = RunValidator(new RadiusAttribute { Id = id }, property);

        Assert.True(actualResult.IsValid);
    }

    [Fact]
    public void Name_Empty_Invalid()
    {
        const string property = "Name";
        var actualResult = RunValidator(new RadiusAttribute { Name = "" }, property);

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
        var actualResult = RunValidator(new RadiusAttribute { Name = new string('*', length) }, property);

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
        var actualResult = RunValidator(new RadiusAttribute { Name = new string('*', length) }, property);

        Assert.True(actualResult.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("=!")]
    [InlineData(">>")]
    [InlineData("!==")]
    [InlineData("!")]
    public void Op_NonExistentOperation_Invalid(string op)
    {
        const string property = "Op";
        var actualResult = RunValidator(new RadiusAttribute { Op = op }, property);

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(property, actualResult.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.NoMatch.Code, actualResult.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(":=")]
    [InlineData("==")]
    [InlineData("+=")]
    [InlineData("!=")]
    [InlineData(">=")]
    [InlineData("<=")]
    [InlineData("=")]
    [InlineData(">")]
    [InlineData("<")]
    [InlineData("=~")]
    [InlineData("!~")]
    [InlineData("=*")]
    [InlineData("!*")]
    public void Op_ExistentOperation_Valid(string op)
    {
        const string property = "Op";
        var actualResult = RunValidator(new RadiusAttribute { Op = op }, property);

        Assert.True(actualResult.IsValid);
    }

    [Theory]
    [InlineData(65)]
    [InlineData(80)]
    public void Value_GreaterThan64_Invalid(int length)
    {
        const string property = "Value";
        var actualResult = RunValidator(new RadiusAttribute { Value = new string('*', length) }, property);

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(property, actualResult.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.FieldExceeded.Code, actualResult.Errors[0].ErrorCode);
    }
}