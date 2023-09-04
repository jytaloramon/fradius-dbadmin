using System.Collections.Immutable;

namespace FradminDomain.Exceptions;

public class UnsatisfiedDependencyException : BaseException
{
    public UnsatisfiedDependencyException(ImmutableDictionary<string, object> errors) : base(errors)
    {
    }
}