using System.Collections.Immutable;

namespace RadiusDomain.Exceptions;

public abstract class BaseException<T> : Exception
{
    protected BaseException(ImmutableDictionary<string, T> errors)
    {
        Errors = errors;
    }

    public ImmutableDictionary<string, T> Errors { get; init; }
}