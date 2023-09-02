using System.Collections.Immutable;

namespace FradminDomain.Exceptions;

public class EntityConflictException : BaseException
{
    public EntityConflictException(ImmutableDictionary<string, object> errors) : base(errors)
    {
    }
}