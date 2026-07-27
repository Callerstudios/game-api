using GameApi.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace GameApi.Handlers;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _environment;
    private readonly IProblemDetailsService _problemDetailsService;

    public GlobalExceptionHandler( ILogger<GlobalExceptionHandler> logger, IHostEnvironment environment, IProblemDetailsService problemDetailsService)
    {
        _logger = logger;
        _environment = environment;
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, exception.Message);

        return exception switch
        {
            ValidationException validationException =>
                await HandleValidationExceptionAsync(httpContext, validationException, cancellationToken),

            AppException appException =>
                await HandleAppExceptionAsync(httpContext, appException, cancellationToken),

            _ =>
                await HandleUnexpectedExceptionAsync(httpContext, exception, cancellationToken)
        };
    }
    private async ValueTask<bool> HandleValidationExceptionAsync(
    HttpContext context,
    ValidationException exception,
    CancellationToken cancellationToken)
    {
        ProblemDetails problem = new()
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation Failed",
            Detail = "One or more validation errors occurred.",
            Type = "https://httpstatuses.com/400",
            Instance = context.Request.Path
        };

        problem.Extensions["errors"] = exception.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray());

        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        await _problemDetailsService.WriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = context,
                ProblemDetails = problem,
                Exception = exception
            });

        return true;
    }
    private async ValueTask<bool> HandleAppExceptionAsync(
    HttpContext context,
    AppException exception,
    CancellationToken cancellationToken)
    {
        ProblemDetails problem = new()
        {
            Status = (int)exception.StatusCode,
            Title = exception.Title,
            Detail = exception.Message,
            Type = exception.Type,
            Instance = context.Request.Path
        };

        problem.Extensions["errorCode"] = exception.ErrorCode;

        context.Response.StatusCode = (int)exception.StatusCode;

        await _problemDetailsService.WriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = context,
                ProblemDetails = problem,
                Exception = exception
            });

        return true;
    }
    private async ValueTask<bool> HandleUnexpectedExceptionAsync(
    HttpContext context,
    Exception exception,
    CancellationToken cancellationToken)
    {
        ProblemDetails problem = new()
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Detail = _environment.IsDevelopment()
                ? exception.Message
                : "An unexpected error occurred.",
            Type = "https://httpstatuses.com/500",
            Instance = context.Request.Path
        };

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await _problemDetailsService.WriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = context,
                ProblemDetails = problem,
                Exception = exception
            });

        return true;
    }
}