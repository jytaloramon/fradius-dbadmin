using System.Collections.Immutable;

namespace RadiusDomain.Exceptions;

public class EntitiesConflictException : BaseMultiException
{
    public EntitiesConflictException(ImmutableDictionary<string, EntityConflictException[]> errors) : base(
        MakeExceptionsMap(
            errors.Select(e => new KeyValuePair<string, BaseException[]>(e.Key, e.Value))))
    {
    }
}