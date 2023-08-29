using FluentValidation;
using FluentValidation.Results;

namespace FradminDomain.UnitTest.Entities.Validations;

public abstract class BaseUnitTestsEntityValidation<T>
{
    private readonly AbstractValidator<T> _validator;

    protected BaseUnitTestsEntityValidation(AbstractValidator<T> validator)
    {
        _validator = validator;
    }

    protected ValidationResult RunValidator(T entity, string property)
    {
        return _validator.Validate(entity, opt => { opt.IncludeProperties(property); });
    }
}