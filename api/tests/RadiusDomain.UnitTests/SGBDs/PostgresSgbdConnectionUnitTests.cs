using RadiusDomain.SGBDs;
using RadiusDomain.SGBDs.Psql;

namespace RadiusDomain.UnitTests.SGBDs;

public static class PostgresSgbdConnectionUnitTests
{
    private const string Host = "172.17.0.2";

    private const string Username = "postgres";

    private const string Password = "postgres";

    private const string Database = "freeradius";

    public static PostgresConnection GetInstance()
    {
        return new PostgresConnection(Host, Username, Password, Database);
    }
}