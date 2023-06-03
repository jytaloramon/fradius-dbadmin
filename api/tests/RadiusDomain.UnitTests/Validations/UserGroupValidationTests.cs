using RadiusDomain.Entities;
using RadiusDomain.Validations;
using RadiusDomain.ValueObjects;

namespace RadiusDomain.UnitTests.EntitiesValidation;

public class UserGroupValidationTests : BaseEntityValidationTests<UserGroup>
{
    public UserGroupValidationTests() : base(new UserGroupValidation())
    {
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Id_LessThan1_Invalid(int id)
    {
        const string property = "Id";
        var actualResult = RunValidator(new UserGroup { Id = id }, property);

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
        var actualResult = RunValidator(new UserGroup { Id = id }, property);

        Assert.True(actualResult.IsValid);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-10)]
    public void Priority_LessThan1_Invalid(int priority)
    {
        const string property = "Priority";
        var actualResult = RunValidator(new UserGroup { Priority = priority }, property);

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(property, actualResult.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.FieldExceeded.Code, actualResult.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(20)]
    [InlineData(128128)]
    public void Priority_GreaterThanOrEquals1_Valid(int priority)
    {
        const string property = "Priority";
        var actualResult = RunValidator(new UserGroup { Priority = priority }, property);

        Assert.True(actualResult.IsValid);
    }
}