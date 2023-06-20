using FluentValidation;
using RadiusDomain.Entities;
using RadiusDomain.Exceptions;
using RadiusDomain.Factories;

namespace RadiusDomain.UnitTests.Factories;

public class GroupFactoryUnitTests : BaseFactoryUnitTests<Group>
{
    public GroupFactoryUnitTests() : base(new InlineValidator<Group>(), new InlineValidator<Group>())
    {
        ValidatorInValid.RuleSet("Name", () => { ValidatorInValid.RuleFor(x => x.Name).Must(_ => false); });
        ValidatorInValid.RuleSet("Attributes", () => { ValidatorInValid.RuleFor(x => x.Attributes).Must(_ => false); });

        ValidatorValid.RuleSet("Name", () => { ValidatorValid.RuleFor(x => x.Name).Must(_ => true); });
        ValidatorValid.RuleSet("Attributes", () => { ValidatorValid.RuleFor(x => x.Attributes).Must(_ => true); });
    }

    [Fact]
    public void CreateStr_InvalidData_ThrowEntityValidationException()
    {
        var factory = new GroupFactory(ValidatorInValid);

        var actualExcept = Assert.Throws<EntityValidationException>(() => { factory.Create("name"); });

        Assert.Single(actualExcept.Errors);
        Assert.True(actualExcept.Errors.ContainsKey("Name"));
    }

    [Fact]
    public void CreateStr_ValidData_ThrowEntityValidationException()
    {
        const string expected = "name";

        var factory = new GroupFactory(ValidatorValid);
        var actualGroup = factory.Create(expected);

        Assert.Equal(expected, actualGroup.Name);
    }

    [Fact]
    public void CreateStrListRadAttr_InvalidData_ThrowEntityValidationException()
    {
        var factory = new GroupFactory(ValidatorInValid);

        var actualExcept = Assert.Throws<EntityValidationException>(() =>
        {
            factory.Create("name", new List<RadiusAttribute>());
        });

        Assert.Equal(2, actualExcept.Errors.Count);
        Assert.True(actualExcept.Errors.ContainsKey("Name"));
        Assert.True(actualExcept.Errors.ContainsKey("Attributes"));
    }

    [Fact]
    public void CreateStrListRadAttr_ValidData_ThrowEntityValidationException()
    {
        const string expected = "name";
        var expectedAttributes = new List<RadiusAttribute>();

        var factory = new GroupFactory(ValidatorValid);
        var actualGroup = factory.Create(expected, expectedAttributes);

        Assert.Equal(expected, actualGroup.Name);
        Assert.Equal(expectedAttributes, actualGroup.Attributes);
    }
}