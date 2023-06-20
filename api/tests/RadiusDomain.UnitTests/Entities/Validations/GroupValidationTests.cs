using RadiusDomain.Entities;
using RadiusDomain.Entities.Validations;
using RadiusDomain.ValueObjects;

namespace RadiusDomain.UnitTests.Entities.Validations;

public class GroupValidationTests : BaseEntityValidationUnitTests<Group>
{
    public GroupValidationTests() : base(new GroupValidation())
    {
    }

    [Fact]
    public void PropertyName_Empty_ReturnValidationResultInvalid()
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
    public void PropertyName_GreaterThan64_ReturnValidationResultInvalid(int length)
    {
        const string property = "Name";

        var actualResult = RunValidator(new Group { Name = new string('*', length) }, property);

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(property, actualResult.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.FieldExceeded.Code, actualResult.Errors[0].ErrorCode);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(64)]
    public void PropertyName_GreaterThan0AndLessThanOrEquals64_ReturnValidationResultValid(int length)
    {
        const string property = "Name";

        var actualResult = RunValidator(new Group { Name = new string('*', length) }, property);

        Assert.True(actualResult.IsValid);
    }

    [Fact]
    public void PropertyAttributes_Null_ReturnValidationResultInvalid()
    {
        const string property = "Attributes";

        var actualResult = RunValidator(new Group { Attributes = null }, property);

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(property, actualResult.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.RequiredProperty.Code, actualResult.Errors[0].ErrorCode);
    }

    [Fact]
    public void PropertyAttributes_EmptyList_ReturnValidationResultInvalid()
    {
        const string property = "Attributes";

        var actualResult = RunValidator(new Group { Attributes = new List<RadiusAttribute>() }, property);

        Assert.False(actualResult.IsValid);
        Assert.Single(actualResult.Errors);
        Assert.Equal(property, actualResult.Errors[0].PropertyName);
        Assert.Equal(ErrorCatalog.RequiredProperty.Code, actualResult.Errors[0].ErrorCode);
    }

    [Fact]
    public void Attributes_NotEmptyList_ReturnValidationResultValid()
    {
        const string property = "Attributes";

        var actualResult = RunValidator(new Group()
        {
            Attributes = new List<RadiusAttribute>
            {
                new()
            }
        }, property);

        Assert.True(actualResult.IsValid);
    }
}