using System.Net;

namespace GameApi.Exceptions.NotFoundException;

public abstract class NotFoundException : AppException
{
    protected NotFoundException(
        string message,
        string errorCode)
        : base(
            message,
            errorCode,
            HttpStatusCode.NotFound,
            "Not Found")
    {
    }
}