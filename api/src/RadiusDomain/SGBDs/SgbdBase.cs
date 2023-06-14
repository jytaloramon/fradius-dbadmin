using System.Data.Common;
using RadiusDomain.SGBDs.Interfaces;

namespace RadiusDomain.SGBDs;

public abstract class SgbdBase
{
    private readonly string _host;

    private readonly string _port;

    private readonly string _username;

    private readonly string _password;

    private readonly string _database;

    private protected SgbdBase(string host, string port, string username, string password, string database,
        ISgbdExceptionHandler exceptionHandlerException)
    {
        _host = host;
        _port = port;
        _username = username;
        _password = password;
        _database = database;
        ExceptionHandler = exceptionHandlerException;
    }

    protected string GetConnectionString =>
        $"Host={_host};Port={_port};Username={_username};Password={_password};Database={_database}";

    public abstract DbConnection GetDbConnection();

    public ISgbdExceptionHandler ExceptionHandler { get; init; }
}