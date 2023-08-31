using System.Data.Common;
using FradminDomain.SGBDs.Handlers;
using Npgsql;

namespace FradminDomain.SGBDs;

public sealed class PostgresConnection : SgbdBase
{
    public PostgresConnection(string host, string username, string password, string database) : base(host, "5432",
        username, password, database, new PostgresExceptionHandler())
    {
    }

    public PostgresConnection(string host, string port, string username, string password, string database) : base(host,
        port, username, password, database, new PostgresExceptionHandler())
    {
    }

    public override DbConnection GetDbConnection()
    {
        return new NpgsqlConnection(GetConnectionString);
    }
}