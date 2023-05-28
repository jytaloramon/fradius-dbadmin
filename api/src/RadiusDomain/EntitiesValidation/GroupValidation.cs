using FluentValidation;
using RadiusDomain.Entities;
using RadiusDomain.ValueObjects;

namespace RadiusDomain.EntitiesValidation;

public class GroupValidation : AbstractValidator<Group>
{
    public GroupValidation()
    {
        RuleSet("Name", () =>
        {
            RuleFor(g => g.Name)
                .NotEmpty()
                .WithErrorCode(ErrorCatalog.RequiredProperty.Code)
                .WithMessage($"{ErrorCatalog.RequiredProperty.Message}: cannot by empty")
                .MaximumLength(64)
                .WithErrorCode(ErrorCatalog.FieldExceeded.Code)
                .WithMessage($"{ErrorCatalog.FieldExceeded.Message}: max (64)");
        });

        RuleSet("Attributes", () =>
        {
            RuleFor(g => g.Attributes)
                .NotEmpty()
                .WithErrorCode(ErrorCatalog.RequiredProperty.Code)
                .WithMessage($"{ErrorCatalog.RequiredProperty.Message}: min (1)");
        });
    }
}