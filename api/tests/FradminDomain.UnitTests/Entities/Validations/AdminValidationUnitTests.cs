using FradminDomain.Entities;
using FradminDomain.Entities.Validations;
using FradminDomain.ValueObjects;

namespace FradminDomain.UnitTest.Entities.Validations;

public class AdminValidationUnitTests : BaseUnitTestsEntityValidation<Admin>
{
    public AdminValidationUnitTests() : base(new AdminValidation())
    {
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-(1 << 30))]
    public void PropertyId_LessThanOrEqualTo0_ReturnInvalidResult(int id)
    {
        const string property = "Id";
        var admin = new Admin { Id = id };
        var actual = RunValidator(admin, property);

        Assert.False(actual.IsValid);
        Assert.Single(actual.Errors);
        Assert.Equal(property, actual.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.OutOfRangeMin.Code, actual.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(1)]
    [InlineData((1 << 30) - 1)]
    public void PropertyId_GreaterThan0_ReturnValidResult(int id)
    {
        var admin = new Admin { Id = id };
        var actual = RunValidator(admin, "Id");

        Assert.True(actual.IsValid);
    }

    [Fact]
    public void PropertyUsername_Empty_ReturnInvalidResult()
    {
        const string property = "Username";
        var admin = new Admin { Username = "" };
        var actual = RunValidator(admin, property);

        Assert.False(actual.IsValid);
        Assert.Single(actual.Errors);
        Assert.Equal(property, actual.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.Required.Code, actual.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(33)]
    [InlineData(64)]
    [InlineData(1024)]
    public void PropertyUsername_GreaterThan32_ReturnInvalidResult(int lengthName)
    {
        const string property = "Username";
        var admin = new Admin { Username = new string('a', lengthName) };
        var actual = RunValidator(admin, property);

        Assert.False(actual.IsValid);
        Assert.Single(actual.Errors);
        Assert.Equal(property, actual.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.OutOfRangeMax.Code, actual.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(16)]
    [InlineData(32)]
    public void PropertyUsername_NonEmptyAndLessThanOrEqualTo32_ReturnValidResult(int lengthName)
    {
        var admin = new Admin { Username = new string('a', lengthName) };
        var actual = RunValidator(admin, "Username");

        Assert.True(actual.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("@")]
    [InlineData("@test14")]
    [InlineData("test14@")]
    [InlineData("test14")]
    public void PropertyEmail_InvalidFormat_ReturnInvalidResult(string email)
    {
        const string property = "Email";
        var admin = new Admin { Email = email };
        var actual = RunValidator(admin, property);

        Assert.False(actual.IsValid);
        Assert.Single(actual.Errors);
        Assert.Equal(property, actual.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.InvalidFormat.Code, actual.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(55)]
    [InlineData(128)]
    [InlineData(1024)]
    public void PropertyEmail_GreaterThan64_ReturnInvalidResult(int lengthName)
    {
        const string property = "Email";
        var admin = new Admin { Email = new string('a', lengthName) + "@com.br.br" };
        var actual = RunValidator(admin, property);

        Assert.False(actual.IsValid);
        Assert.Single(actual.Errors);
        Assert.Equal(property, actual.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.OutOfRangeMax.Code, actual.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData("test@.com")]
    [InlineData("t14_-9est@.com")]
    [InlineData("test@br.com")]
    public void PropertyEmail_ValidFormat_ReturnValidResult(string email)
    {
        var admin = new Admin { Email = email };
        var actual = RunValidator(admin, "Email");

        Assert.True(actual.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("01234567891")]
    public void PropertyPassword_LessThan12_ReturnInvalidResult(string password)
    {
        const string property = "Password";
        var admin = new Admin { Password = password };
        var actual = RunValidator(admin, property);

        Assert.False(actual.IsValid);
        Assert.Single(actual.Errors);
        Assert.Equal(property, actual.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.OutOfRangeMin.Code, actual.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(33)]
    [InlineData(64)]
    [InlineData(1024)]
    public void PropertyPassword_GreaterThan32_ReturnInvalidResult(int lengthName)
    {
        const string property = "Password";
        var admin = new Admin { Password = new string('a', lengthName) };
        var actual = RunValidator(admin, property);

        Assert.False(actual.IsValid);
        Assert.Single(actual.Errors);
        Assert.Equal(property, actual.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.OutOfRangeMax.Code, actual.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData("012345678912")]
    [InlineData("0123456789123456")]
    [InlineData("01234567891234560123456789123456")]
    public void PropertyPassword_GreateThan12AndLessThanOrEqualTo32_ReturnValidResult(string password)
    {
        const string property = "Password";
        var admin = new Admin { Password = password };
        var actual = RunValidator(admin, property);

        Assert.True(actual.IsValid);
    }
}
