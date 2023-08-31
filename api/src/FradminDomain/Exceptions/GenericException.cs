using System.Collections.Immutable;

namespace FradminDomain.Exceptions;

public class GenericException:BaseException
{
    public GenericException(ImmutableDictionary<string, object> errors) : base(errors)
    {
    }
}