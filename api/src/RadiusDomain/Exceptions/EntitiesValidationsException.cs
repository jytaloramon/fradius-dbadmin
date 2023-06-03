using System.Collections.Immutable;

namespace RadiusDomain.Exceptions;

public class EntitiesValidationsException : BaseMultiException
{
    public EntitiesValidationsException(ImmutableDictionary<string, EntityValidationException[]> errors) : base(
        MakeExceptionsMap(
            errors.Select(e => new KeyValuePair<string, BaseException[]>(e.Key, e.Value))))
    {
    }
}