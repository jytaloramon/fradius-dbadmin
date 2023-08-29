using System.Collections.Immutable;
using FluentValidation;
using FradminDomain.Entities;
using FradminDomain.Exceptions;
using FradminDomain.Factories;
using FradminDomain.ValueObjects;

namespace FradminDomain.UnitTest.Factories;

public class AdminGroupFactoryUnitTests : BaseUnitTestsFactory<AdminGroup>
{
    public override InlineValidator<AdminGroup> GetValidatorAllInvalid()
    {
        var inlineVal = new InlineValidator<AdminGroup>();
        inlineVal.RuleSet("Id", () => { inlineVal.RuleFor(group => group.Id).Must(s => false); });
        inlineVal.RuleSet("Name", () => { inlineVal.RuleFor(group => group.Name).Must(s => false); });
        inlineVal.RuleSet("Rules", () => { inlineVal.RuleFor(group => group.Rules).Must(set => false); });

        return inlineVal;
    }

    public override InlineValidator<AdminGroup> GetValidatorAllValid()
    {
        var inlineVal = new InlineValidator<AdminGroup>();
        inlineVal.RuleSet("Id", () => { inlineVal.RuleFor(group => group.Id).Must(s => true); });
        inlineVal.RuleSet("Name", () => { inlineVal.RuleFor(group => group.Name).Must(s => true); });
        inlineVal.RuleSet("Rules", () => { inlineVal.RuleFor(group => group.Rules).Must(set => true); });

        return inlineVal;
    }

    [Fact]
    public void Create_NameAndRules_ThrowEntityValidationException()
    {
        var factory = new AdminGroupFactory(GetValidatorAllInvalid());
        var actual = Assert.Throws<EntityValidationException>(() => { factory.Create("a", new HashSet<Rules>()); });

        Assert.Equal(2, actual.Errors.Count);
        Assert.True(actual.Errors.ContainsKey("Name"));
        Assert.True(actual.Errors.ContainsKey("Rules"));
    }

    [Fact]
    public void Create_NameAndRules_ReturnEntity()
    {
        const string name = "a";
        var rules = new[] { (Rules)1, (Rules)2 };

        var factory = new AdminGroupFactory(GetValidatorAllValid());
        var actual = factory.Create(name, new HashSet<Rules>(rules));

        Assert.Equal(name, actual.Name);
        Assert.True(actual.Rules.SetEquals(rules.ToImmutableList()));
    }
}