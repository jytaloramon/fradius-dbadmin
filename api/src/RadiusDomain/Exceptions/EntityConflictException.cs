using System.Collections.Immutable;

namespace RadiusDomain.Exceptions;

public class EntityConflictException : BaseException<string>
{
    public EntityConflictException(ImmutableDictionary<string, string> errors) : base(errors)
    {
    }
}