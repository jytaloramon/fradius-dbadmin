using FluentValidation;
using RadiusDomain.Entities;
using RadiusDomain.Exceptions;
using RadiusDomain.Factories;

namespace RadiusDomain.UnitTests.Factories;

public class UserFactoryTests : BaseFactoryUnitTests<User>
{
    public UserFactoryTests() : base(new InlineValidator<User>(), new InlineValidator<User>())
    {
        ValidatorInValid.RuleFor(u => u.Username).Must(_ => false);
        ValidatorInValid.RuleFor(u => u.Attributes).Must(_ => false);
        ValidatorInValid.RuleFor(u => u.Groups).Must(_ => false);

        ValidatorValid.RuleFor(u => u.Username).Must(_ => true);
        ValidatorValid.RuleFor(u => u.Attributes).Must(_ => true);
        ValidatorValid.RuleFor(u => u.Groups).Must(_ => true);
    }

    [Fact]
    public void Create_FailureAllProperty_ThrowsEntityValidationException()
    {
        var factory = new UserFactory(ValidatorValid);

        var actualErrors = Assert.Throws<EntityValidationException>(() =>
        {
            factory.Create("*", new List<RadiusAttribute>());
        });
        Assert.Equal(2, actualErrors.Errors.Count());
    }

    [Fact]
    public void Create_Success_ReturnAnEntity()
    {
        const string username = "username";
        var attributes = new List<RadiusAttribute>();

        var factory = new UserFactory(ValidatorValid);
        var actualUser = factory.Create(username, attributes);

        Assert.Equal(username, actualUser.Username);
        Assert.Equal(attributes, actualUser.Attributes);
    }
}