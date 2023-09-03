using FluentValidation;
using FradminDomain.Entities;
using FradminDomain.Exceptions;
using FradminDomain.Factories;

namespace FradminDomain.UnitTest.Factories;

public class AdminFactoryUnitTests : BaseUnitTestsFactory<Admin>
{
    public override InlineValidator<Admin> GetValidatorAllInvalid()
    {
        var inlineVal = new InlineValidator<Admin>();
        inlineVal.RuleSet("Id", () => { inlineVal.RuleFor(group => group.Id).Must(_ => false); });
        inlineVal.RuleSet("Username", () => { inlineVal.RuleFor(group => group.Username).Must(_ => false); });
        inlineVal.RuleSet("Email", () => { inlineVal.RuleFor(group => group.Email).Must(_ => false); });
        inlineVal.RuleSet("Password", () => { inlineVal.RuleFor(group => group.Password).Must(_ => false); });

        return inlineVal;
    }

    public override InlineValidator<Admin> GetValidatorAllValid()
    {
        var inlineVal = new InlineValidator<Admin>();
        inlineVal.RuleSet("Id", () => { inlineVal.RuleFor(group => group.Id).Must(_ => true); });
        inlineVal.RuleSet("Username", () => { inlineVal.RuleFor(group => group.Username).Must(_ => true); });
        inlineVal.RuleSet("Email", () => { inlineVal.RuleFor(group => group.Email).Must(_ => true); });
        inlineVal.RuleSet("Password", () => { inlineVal.RuleFor(group => group.Password).Must(_ => true); });

        return inlineVal;
    }

    [Fact]
    public void Create_UserEmailPassGroupAndIsActiveWithAllInvalid_ThrowEntityValidationException()
    {
        var factory = new AdminFactory(GetValidatorAllInvalid());

        var except = Assert.Throws<EntityValidationException>(() =>
        {
            factory.Create("a", "a@a", "1234", new AdminGroup(), true);
        });

        Assert.Equal(3, except.Errors.Count);
        Assert.True(except.Errors.ContainsKey("Username"));
        Assert.True(except.Errors.ContainsKey("Email"));
        Assert.True(except.Errors.ContainsKey("Password"));
    }

    [Fact]
    public void Create_UserEmailPassGroupAndIsActiveWithAllValid_ReturnTheEntity()
    {
        const string username = "a";
        const string email = "a@a";
        const string password = "1234";
        var adminGroup = new AdminGroup();
        const bool isActive = true;

        var factory = new AdminFactory(GetValidatorAllValid());
        var actual = factory.Create(username, email, password, adminGroup, isActive);

        Assert.Equal(username, actual.Username);
        Assert.Equal(email, actual.Email);
        Assert.Equal(password, actual.Password);
        Assert.Equal(adminGroup, actual.Group);
        Assert.Equal(isActive, actual.IsActive);
    }
}