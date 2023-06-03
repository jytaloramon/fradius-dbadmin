using System.Collections.Immutable;

namespace RadiusDomain.Exceptions;

public abstract class BaseMultiException : Exception
{
    protected BaseMultiException(ImmutableDictionary<string, BaseException[]> errors)
    {
        Errors = errors;
    }

    public ImmutableDictionary<string, BaseException[]> Errors { get; init; }

    protected static ImmutableDictionary<string, BaseException[]> MakeExceptionsMap(
        IEnumerable<KeyValuePair<string, BaseException[]>> errors)
    {
        return (new Dictionary<string, BaseException[]>(errors)).ToImmutableDictionary();
    }
}