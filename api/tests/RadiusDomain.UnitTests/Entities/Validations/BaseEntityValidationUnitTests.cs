using FluentValidation;
using FluentValidation.Results;

namespace RadiusDomain.UnitTests.Entities.Validations;

public abstract class BaseEntityValidationUnitTests<T>
{
    private readonly AbstractValidator<T> _validator;

    protected BaseEntityValidationUnitTests(AbstractValidator<T> validator)
    {
        _validator = validator;
    }

    protected ValidationResult RunValidator(T entity, string property)
    {
        return _validator.Validate(entity, opt => { opt.IncludeProperties(property); });
    }
}