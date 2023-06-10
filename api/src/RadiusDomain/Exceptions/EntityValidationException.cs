using System.Collections.Immutable;

namespace RadiusDomain.Exceptions;

public class EntityValidationException : BaseException<object>
{
    public EntityValidationException(ImmutableDictionary<string, object> errors) : base(errors)
    {
    }
}