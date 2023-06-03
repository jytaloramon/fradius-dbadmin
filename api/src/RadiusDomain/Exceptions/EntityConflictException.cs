using System.Collections.Immutable;
using RadiusDomain.Entities;

namespace RadiusDomain.Exceptions;

public class EntityConflictException:BaseException
{
    public EntityConflictException(ImmutableDictionary<string, ErrorMessage[]> errors) : base(errors)
    {
    }
}