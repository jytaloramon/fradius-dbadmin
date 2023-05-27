using FluentValidation;
using RadiusDomain.Entities;
using RadiusDomain.EntitiesValidation;
using RadiusDomain.ValueObjects;

namespace RadiusDomain.UnitTests.EntitiesValidation;

public class UserValidationTests
{
    private readonly UserValidation _userValidator;

    public UserValidationTests()
    {
        _userValidator = new UserValidation();
    }

    [Fact]
    public void Username_Empty_Invalid()
    {
        var user = new User { Username = "" };
        var actualResult = _userValidator.Validate(user, opt => { opt.IncludeProperties("Username"); });

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(actualResult.Errors[0].ErrorCode, ErrorCatalog.RequiredProperty.Code);
    }

    [Theory]
    [InlineData(65)]
    [InlineData(80)]
    public void Username_GreaterThan65_Invalid(int length)
    {
        var user = new User { Username = new string('*', length) };
        var actualResult = _userValidator.Validate(user, opt => { opt.IncludeProperties("Username"); });

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(actualResult.Errors[0].ErrorCode, ErrorCatalog.FieldExceeded.Code);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(63)]
    public void Username_GreaterThan0AndLessThan64_Valid(int length)
    {
        var user = new User { Username = new string('*', length) };
        var actualResult = _userValidator.Validate(user, opt => { opt.IncludeProperties("Username"); });

        Assert.True(actualResult.IsValid);
    }

    [Fact]
    public void Attributes_EmptyList_Invalid()
    {
        var user = new User { Attributes = { } };
        var actualResult = _userValidator.Validate(user, opt => { opt.IncludeProperties("Attributes"); });

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(actualResult.Errors[0].ErrorCode, ErrorCatalog.RequiredProperty.Code);
    }

    [Fact]
    public void Attributes_NotEmptyList_Valid()
    {
        var user = new User { Attributes = { new RadiusAttribute() } };
        var actualResult = _userValidator.Validate(user, opt => { opt.IncludeProperties("Attributes"); });

        Assert.True(actualResult.IsValid);
    }
}