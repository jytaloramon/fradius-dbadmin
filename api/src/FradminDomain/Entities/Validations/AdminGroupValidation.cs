using FluentValidation;
using FradminDomain.ValueObjects;

namespace FradminDomain.Entities.Validations;

public class AdminGroupValidation : AbstractValidator<AdminGroup>
{
    public AdminGroupValidation()
    {
        RuleSet("Id", () =>
        {
            RuleFor(group => group.Id)
                .GreaterThan((short)0)
                .WithErrorCode(ErrorCatalog.OutOfRangeMin.Code)
                .WithMessage(ErrorCatalog.OutOfRangeMin.Message);
        });

        RuleSet("Name", () =>
        {
            RuleFor(group => group.Name)
                .NotEmpty()
                .WithErrorCode(ErrorCatalog.Required.Code)
                .WithMessage(ErrorCatalog.Required.Message)
                .MaximumLength(32)
                .WithErrorCode(ErrorCatalog.OutOfRangeMax.Code)
                .WithMessage(ErrorCatalog.OutOfRangeMax.Message);
        });

        RuleSet("Rules", () =>
        {
            RuleFor(group => group.Rules)
                .NotEmpty()
                .WithErrorCode(ErrorCatalog.Required.Code)
                .WithMessage(ErrorCatalog.Required.Message)
                .ForEach(collection => collection.IsInEnum()
                    .WithErrorCode(ErrorCatalog.NotFound.Code)
                    .WithMessage(ErrorCatalog.NotFound.Message));
        });
    }
}