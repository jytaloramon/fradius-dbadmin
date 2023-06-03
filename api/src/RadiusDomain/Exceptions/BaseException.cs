using System.Collections.Immutable;
using RadiusDomain.Entities;

namespace RadiusDomain.Exceptions;

public abstract class BaseException : Exception
{
    protected BaseException(ImmutableDictionary<string, ErrorMessage[]> errors)
    {
        Errors = errors;
    }

    public ImmutableDictionary<string, ErrorMessage[]> Errors { get; init; }
}