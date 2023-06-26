using FluentValidation;
using RadiusDomain.Entities;
using RadiusDomain.Factories.Interfaces;

namespace RadiusDomain.Factories;

public class UserFactory : BaseFactory<User>, IUserFactory
{
    public UserFactory(AbstractValidator<User> validator) : base(validator)
    {
    }

    public User Create(string username, List<RadiusAttribute> attributes)
    {
        var user = new User { Username = username, Attributes = attributes };
        var resultValidation = RunValidator(user, "Username", "Attributes");

        if (resultValidation.IsValid) return user;

        var errorsPairs = resultValidation.Errors.GroupBy(vf => vf.PropertyName)
            .Select(vfGp => new KeyValuePair<string, object>(vfGp.Key,
                vfGp.Select(failure => new ErrorMessage(failure.ErrorCode, failure.ErrorMessage)).ToArray()));

        throw CreateEntityValidationException(errorsPairs);
    }
}