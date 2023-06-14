using System.Data.Common;
using RadiusDomain.Exceptions;

namespace RadiusDomain.SGBDs.Interfaces;

public interface ISgbdExceptionHandler
{
    public BaseException<object> Handler(DbException dbException);
}