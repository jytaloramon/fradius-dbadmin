using FluentValidation;
using RadiusDomain.Entities;
using RadiusDomain.Exceptions;
using RadiusDomain.Factories;

namespace RadiusDomain.UnitTests.Factories;

public class UserFactoryTests : BaseFactoryUnitTests<User>
{
    public UserFactoryTests() : base(new InlineValidator<User>(), new InlineValidator<User>())
    {
        ValidatorAllValid.RuleFor(u => u.Username).Must(_ => true);
        ValidatorAllValid.RuleFor(u => u.Attributes).Must(_ => true);
        ValidatorAllValid.RuleFor(u => u.Groups).Must(_ => true);

        ValidatorAllInvalid.RuleFor(u => u.Username).Must(_ => false);
        ValidatorAllInvalid.RuleFor(u => u.Attributes).Must(_ => false);
        ValidatorAllInvalid.RuleFor(u => u.Groups).Must(_ => false);
    }

    [Fact]
    public void Create_FailureAllProperty_ThrowsEntityValidationException()
    {
        var factory = new UserFactory(ValidatorAllInvalid);

        var actualErrors = Assert.Throws<EntityValidationException>(() =>
        {
            factory.Create("*", new List<RadiusAttribute>(), new List<UserGroup>());
        });
        Assert.Equal(2, actualErrors.Errors.Count());
    }

    [Fact]
    public void Create_Success_ReturnAnEntity()
    {
        const string username = "username";
        var attributes = new List<RadiusAttribute>();
        var groups = new List<UserGroup>();

        var factory = new UserFactory(ValidatorAllValid);
        var actualUser = factory.Create(username, attributes, groups);

        Assert.Equal(username, actualUser.Username);
        Assert.Equal(attributes, actualUser.Attributes);
        Assert.Equal(groups, actualUser.Groups);
    }
}