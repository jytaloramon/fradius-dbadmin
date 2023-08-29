using System.Collections.Immutable;

namespace FradminDomain.Exceptions;

public class EntityValidationException : BaseException
{
    public EntityValidationException(ImmutableDictionary<string, object> errors) : base(errors)
    {
    }
}