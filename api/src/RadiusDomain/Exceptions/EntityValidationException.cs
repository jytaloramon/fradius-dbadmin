using System.Collections.Immutable;
using RadiusDomain.Entities;

namespace RadiusDomain.Exceptions;

public class EntityValidationException : BaseException
{
    public EntityValidationException(ImmutableDictionary<string, ErrorMessage[]> errors) : base(errors)
    {
    }
}