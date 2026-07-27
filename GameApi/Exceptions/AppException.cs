using System.Net;

namespace GameApi.Exceptions;

public abstract class AppException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public string ErrorCode { get; }

    public string Title { get; }

    public virtual string Type => $"https://httpstatuses.com/{(int)StatusCode}";

    protected AppException(string message, string errorCode, HttpStatusCode statusCode, string title): base(message)
    {
        StatusCode = statusCode;
        Title = title;
        ErrorCode = errorCode;
    }
}