using System.Net;

namespace GameApi.Exceptions.UnauthorizedException;

public abstract class UnauthorizedException : AppException
{
    protected UnauthorizedException(
        string message,
        string errorCode)
        : base(
            message,
            errorCode,
            HttpStatusCode.Unauthorized,
            "Unauthorized")
    {
    }
}