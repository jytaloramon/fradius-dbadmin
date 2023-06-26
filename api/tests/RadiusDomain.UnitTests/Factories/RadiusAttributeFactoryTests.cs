using FluentValidation;
using RadiusDomain.Entities;
using RadiusDomain.Exceptions;
using RadiusDomain.Factories;

namespace RadiusDomain.UnitTests.Factories;

public class RadiusAttributeFactoryTests : BaseFactoryUnitTests<RadiusAttribute>
{
    public RadiusAttributeFactoryTests() : base(new InlineValidator<RadiusAttribute>(),
        new InlineValidator<RadiusAttribute>())
    {
        ValidatorInValid.RuleFor(attr => attr.Name).Must(_ => false);
        ValidatorInValid.RuleFor(attr => attr.Op).Must(_ => false);
        ValidatorInValid.RuleFor(attr => attr.Value).Must(_ => false);

        ValidatorValid.RuleFor(attr => attr.Name).Must(_ => true);
        ValidatorValid.RuleFor(attr => attr.Op).Must(_ => true);
        ValidatorValid.RuleFor(attr => attr.Value).Must(_ => true);
    }

    [Fact]
    public void Create_FailureAllProperty_ThrowsEntityValidationException()
    {
        var factory = new RadiusAttributeFactory(ValidatorValid);

        var actualErrors = Assert.Throws<EntityValidationException>(() => { factory.Create("*", "*", "*"); });
        Assert.Equal(4, actualErrors.Errors.Count());
    }

    [Fact]
    public void Create_Success_ReturnAnEntity()
    {
        const string name = "name";
        const string op = "op";
        const string value = "value";

        var factory = new RadiusAttributeFactory(ValidatorValid);
        var actualAttribute = factory.Create(name, op, value);

        Assert.Equal(name, actualAttribute.Name);
        Assert.Equal(op, actualAttribute.Op);
        Assert.Equal(value, actualAttribute.Value);
    }
}