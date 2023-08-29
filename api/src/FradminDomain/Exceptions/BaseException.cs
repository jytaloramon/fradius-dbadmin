using System.Collections.Immutable;

namespace FradminDomain.Exceptions;

public abstract class BaseException : Exception
{
    protected BaseException(ImmutableDictionary<string, object> errors)
    {
        Errors = errors;
    }

    public ImmutableDictionary<string, object> Errors { get; init; }
}