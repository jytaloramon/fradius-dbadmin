using System.Data.Common;
using FradminDomain.Exceptions;

namespace FradminDomain.SGBDs.Interfaces;

public interface ISgbdExceptionHandler
{
    public BaseException Handler(DbException dbException);
}