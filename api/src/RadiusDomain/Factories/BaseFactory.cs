using FluentValidation;
using FluentValidation.Results;
using RadiusDomain.Entities;
using RadiusDomain.Exceptions;

namespace RadiusDomain.Factories;

public abstract class BaseFactory<T>
{
    private readonly AbstractValidator<T> _validator;

    protected BaseFactory(AbstractValidator<T> validator)
    {
        _validator = validator;
    }

    protected ValidationResult RunValidator(T entity, params string[] properties)
    {
        return _validator.Validate(entity, opt => { opt.IncludeProperties(properties); });
    }

    protected EntityValidationException CreateEntityException(IEnumerable<ValidationFailure> failures)
    {
        var errors = failures.GroupBy(f => f.PropertyName)
            .Select(group => new KeyValuePair<string, ErrorMessage[]>(group.Key,
                group.Select(vf => new ErrorMessage(vf.ErrorCode, vf.ErrorMessage)).ToArray()));

        return new EntityValidationException(new Dictionary<string, ErrorMessage[]>(errors));
    }
}