using System.Net;

namespace GameApi.Exceptions.ConflictException;

public abstract class ConflictException : AppException
{
    protected ConflictException(
        string message,
        string errorCode)
        : base(
            message,
            errorCode,
            HttpStatusCode.Conflict,
            "Conflict")
    {
    }
}