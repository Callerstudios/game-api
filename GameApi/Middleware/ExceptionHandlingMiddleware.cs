using GameApi.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GameApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "An unhandled exception occurred.");

            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        ProblemDetails problem;

        if (exception is AppException appException)
        {
            context.Response.StatusCode = (int)appException.StatusCode;

            problem = new ProblemDetails
            {
                Status = (int)appException.StatusCode,
                Title = appException.Title,
                Detail = appException.Message,
                Instance = context.Request.Path
            };
        }
        else
        {
            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            problem = new ProblemDetails
            {
                Status = 500,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred.",
                Instance = context.Request.Path
            };
        }

        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problem);
    }
}