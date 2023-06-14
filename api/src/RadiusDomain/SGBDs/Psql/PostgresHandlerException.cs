using System.Collections.Immutable;
using System.Data.Common;
using Npgsql;
using RadiusDomain.Exceptions;
using RadiusDomain.SGBDs.Interfaces;

namespace RadiusDomain.SGBDs.Psql;

public sealed class PostgresHandlerException : ISgbdExceptionHandler
{
    public BaseException<object> Handler(DbException dbException)
    {
        var psqlExcept = (PostgresException)dbException;

        return psqlExcept.SqlState switch
        {
            "22001" => new EntityValidationException(new Dictionary<string, object>().ToImmutableDictionary()),
            _ => null!
        };
    }
}