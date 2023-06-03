using FluentValidation;
using RadiusDomain.Entities;
using RadiusDomain.ValueObjects;

namespace RadiusDomain.Validations;

public class UserGroupValidation : AbstractValidator<UserGroup>
{
    public UserGroupValidation()
    {
        RuleSet("Id", () =>
        {
            RuleFor(ug => ug.Id)
                .GreaterThanOrEqualTo(1)
                .WithErrorCode(ErrorCatalog.FieldExceeded.Code)
                .WithMessage($"{ErrorCatalog.FieldExceeded.Message}: min (1)");
        });

        RuleSet("Priority", () =>
        {
            RuleFor(ug => ug.Priority)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode(ErrorCatalog.FieldExceeded.Code)
                .WithMessage($"{ErrorCatalog.FieldExceeded.Message}: min (0)");
        });
    }
}