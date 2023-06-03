using RadiusDomain.Entities;
using RadiusDomain.Validations;
using RadiusDomain.ValueObjects;

namespace RadiusDomain.UnitTests.Validations;

public class UserValidationTests : BaseEntityValidationTests<User>
{
    public UserValidationTests() : base(new UserValidation())
    {
    }

    [Fact]
    public void Username_Empty_Invalid()
    {
        const string property = "Username";
        var actualResult = RunValidator(new User { Username = "" }, property);

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(property, actualResult.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.RequiredProperty.Code, actualResult.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(65)]
    [InlineData(80)]
    public void Username_GreaterThan64_Invalid(int length)
    {
        const string property = "Username";
        var actualResult = RunValidator(new User { Username = new string('*', length) }, property);

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(property, actualResult.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.FieldExceeded.Code, actualResult.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(64)]
    public void Username_GreaterThan0AndLessThanOrEquals64_Valid(int length)
    {
        const string property = "Username";
        var actualResult = RunValidator(new User { Username = new string('*', length) }, property);

        Assert.True(actualResult.IsValid);
    }

    [Fact]
    public void Attributes_EmptyList_Invalid()
    {
        const string property = "Attributes";
        var actualResult = RunValidator(new User { Attributes = { } }, property);

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(property, actualResult.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.RequiredProperty.Code, actualResult.Errors[0].ErrorCode);
    }

    [Fact]
    public void Attributes_NotEmptyList_Valid()
    {
        const string property = "Attributes";
        var actualResult = RunValidator(new User { Attributes = { new RadiusAttribute() } }, property);

        Assert.True(actualResult.IsValid);
    }
}