using System.Collections.Immutable;
using FluentValidation;
using FluentValidation.Results;
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

    protected EntityValidationException CreateEntityValidationException(
        IEnumerable<KeyValuePair<string, object>> errors)
    {
        return new EntityValidationException(new Dictionary<string, object>(errors).ToImmutableDictionary());
    }
}