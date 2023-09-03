using FluentValidation;
using FradminDomain.ValueObjects;

namespace FradminDomain.Entities.Validations;

public class AdminValidation : AbstractValidator<Admin>
{
    public AdminValidation()
    {
        RuleSet("Id", () =>
        {
            RuleFor(admin => admin.Id)
                .GreaterThan(0)
                .WithErrorCode(ErrorCatalog.OutOfRangeMin.Code)
                .WithMessage(ErrorCatalog.OutOfRangeMin.Message);
        });

        RuleSet("Username", () =>
        {
            RuleFor(admin => admin.Username)
                .NotEmpty()
                .WithErrorCode(ErrorCatalog.Required.Code)
                .WithMessage(ErrorCatalog.Required.Message)
                .MaximumLength(32)
                .WithErrorCode(ErrorCatalog.OutOfRangeMax.Code)
                .WithMessage(ErrorCatalog.OutOfRangeMax.Message);
        });

        RuleSet("Email", () =>
        {
            RuleFor(admin => admin.Email)
                .MaximumLength(64)
                .WithErrorCode(ErrorCatalog.OutOfRangeMax.Code)
                .WithMessage(ErrorCatalog.OutOfRangeMax.Message)
                .EmailAddress()
                .WithErrorCode(ErrorCatalog.InvalidFormat.Code)
                .WithMessage(ErrorCatalog.InvalidFormat.Message);
        });

        RuleSet("Password", () =>
        {
            RuleFor(admin => admin.Password)
                .MinimumLength(12)
                .WithErrorCode(ErrorCatalog.OutOfRangeMin.Code)
                .WithMessage(ErrorCatalog.OutOfRangeMin.Message)
                .MaximumLength(32)
                .WithErrorCode(ErrorCatalog.OutOfRangeMax.Code)
                .WithMessage(ErrorCatalog.OutOfRangeMax.Message);
        });
    }
}