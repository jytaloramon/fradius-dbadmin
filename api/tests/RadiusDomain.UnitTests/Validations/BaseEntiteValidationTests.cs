using FluentValidation;
using FluentValidation.Results;

namespace RadiusDomain.UnitTests.EntitiesValidation;

public abstract class BaseEntityValidationTests<T>
{
    private readonly AbstractValidator<T> _validator;

    protected BaseEntityValidationTests(AbstractValidator<T> validator)
    {
        _validator = validator;
    }

    protected ValidationResult RunValidator(T entity, string property)
    {
        return _validator.Validate(entity, opt => { opt.IncludeProperties(property); });
    }
}