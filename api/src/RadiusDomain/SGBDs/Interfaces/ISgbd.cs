using System.Data.Common;

namespace RadiusDomain.SGBDs.Interfaces;

public interface ISgbd
{
    public DbConnection GetDbConnection();

    public ISgbdExceptionHandler ExceptionHandler { get; init; }
}