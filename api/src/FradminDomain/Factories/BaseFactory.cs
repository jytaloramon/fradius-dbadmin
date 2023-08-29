using System.Collections.Immutable;
using System.Text.RegularExpressions;
using FluentValidation;
using FradminDomain.Exceptions;
using FluentValidation.Results;
using FradminDomain.Entities;

namespace FradminDomain.Factories;

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

    protected EntityValidationException CreateEntityValidationException(List<ValidationFailure> errors)
    {
        const string reEndArray = @".+\[[0-9]+\]$";
        var dic = new Dictionary<string, object>();

        foreach (var failure in errors)
        {
            if (Regex.IsMatch(failure.PropertyName, reEndArray))
            {
                var indexSquareOpen = failure.PropertyName.LastIndexOf("[", StringComparison.Ordinal);

                var attr = failure.PropertyName[..indexSquareOpen];
                var index = failure.PropertyName[(indexSquareOpen + 1)..^1];

                if (!dic.ContainsKey(attr))
                {
                    dic.Add(attr, new Dictionary<string, ErrorFormat>());
                }

                var e = (Dictionary<string, ErrorFormat>)dic[attr];
                e.Add(index, new ErrorFormat(failure.ErrorCode, failure.ErrorMessage));

                continue;
            }

            if (!dic.ContainsKey(failure.PropertyName))
            {
                dic.Add(failure.PropertyName, new ErrorFormat(failure.ErrorCode, failure.ErrorMessage));
            }
        }

        return new EntityValidationException(dic.ToImmutableDictionary());
    }
}