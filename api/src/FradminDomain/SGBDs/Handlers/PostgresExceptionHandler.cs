using System.Data.Common;
using FradminDomain.Exceptions;
using FradminDomain.SGBDs.Interfaces;
using Npgsql;

namespace FradminDomain.SGBDs.Handlers;

public sealed class PostgresExceptionHandler : ISgbdExceptionHandler
{
    public BaseException Handler(DbException dbException)
    {
        var psqlExcept = (PostgresException)dbException;

        throw new NotImplementedException();
    }
}