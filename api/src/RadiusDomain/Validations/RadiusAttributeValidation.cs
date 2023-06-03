using FluentValidation;
using RadiusDomain.Entities;
using RadiusDomain.ValueObjects;

namespace RadiusDomain.Validations;

public class RadiusAttributeValidation : AbstractValidator<RadiusAttribute>
{
    public RadiusAttributeValidation()
    {
        RuleSet("Id", () =>
        {
            RuleFor(attr => attr.Id)
                .GreaterThanOrEqualTo(1)
                .WithErrorCode(ErrorCatalog.FieldExceeded.Code)
                .WithMessage($"{ErrorCatalog.FieldExceeded.Message}: min (1)");
        });

        RuleSet("Owner", () =>
        {
            RuleFor(attr => attr.Owner)
                .NotEmpty()
                .WithErrorCode(ErrorCatalog.RequiredProperty.Code)
                .WithMessage($"{ErrorCatalog.RequiredProperty.Message}: cannot by empty")
                .MaximumLength(64)
                .WithErrorCode(ErrorCatalog.FieldExceeded.Code)
                .WithMessage($"{ErrorCatalog.FieldExceeded.Message}: max (64)");
        });
        
        RuleSet("Name", () =>
        {
            RuleFor(attr => attr.Name)
                .NotEmpty()
                .WithErrorCode(ErrorCatalog.RequiredProperty.Code)
                .WithMessage($"{ErrorCatalog.RequiredProperty.Message}: cannot by empty")
                .MaximumLength(64)
                .WithErrorCode(ErrorCatalog.FieldExceeded.Code)
                .WithMessage($"{ErrorCatalog.FieldExceeded.Message}: max (64)");
        });

        RuleSet("Op", () =>
        {
            RuleFor(attr => attr.Op)
                .Matches(RegexPatternCatalog.RadiusOp)
                .WithErrorCode(ErrorCatalog.NoMatch.Code)
                .WithMessage($"{ErrorCatalog.NoMatch.Message}: non-existent operation");
        });

        RuleSet("Value", () =>
        {
            RuleFor(attr => attr.Value)
                .MaximumLength(64)
                .WithErrorCode(ErrorCatalog.FieldExceeded.Code)
                .WithMessage($"{ErrorCatalog.FieldExceeded.Message}: max (64)");
        });
    }
}