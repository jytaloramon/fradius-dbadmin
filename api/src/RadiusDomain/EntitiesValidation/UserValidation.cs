using FluentValidation;
using RadiusDomain.Entities;
using RadiusDomain.ValueObjects;

namespace RadiusDomain.EntitiesValidation;

public class UserValidation : AbstractValidator<User>
{
    public UserValidation()
    {
        RuleSet("Username", () =>
        {
            RuleFor(u => u.Username)
                .NotEmpty()
                .WithErrorCode(ErrorCatalog.RequiredProperty.Code)
                .WithMessage($"{ErrorCatalog.RequiredProperty.Message}: cannot by empty")
                .MaximumLength(64)
                .WithErrorCode(ErrorCatalog.FieldExceeded.Code)
                .WithMessage($"{ErrorCatalog.FieldExceeded.Message}: max (64)");
        });

        RuleSet("Attributes", () =>
        {
            RuleFor(u => u.Attributes)
                .NotEmpty()
                .WithErrorCode(ErrorCatalog.RequiredProperty.Code)
                .WithMessage($"{ErrorCatalog.RequiredProperty.Message}: min (1)");
        });
    }
}