using System.Data.Common;
using Npgsql;

namespace RadiusDomain.SGBDs.Psql;

public sealed class PostgresConnection : SgbdBase
{
    public PostgresConnection(string host, string username, string password, string database) : base(host, "5432",
        username, password, database, new PostgresHandlerException())
    {
    }

    public PostgresConnection(string host, string port, string username, string password, string database) : base(host,
        port, username, password, database, new PostgresHandlerException())
    {
    }

    public override DbConnection GetDbConnection()
    {
        return new NpgsqlConnection(GetConnectionString);
    }
}