using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Middlewares;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, exception.Message);

        int status = StatusCodes.Status500InternalServerError;
        ProblemDetails problemDetails;

        switch (exception)
        {
            case NotFoundException:
                status = StatusCodes.Status404NotFound;
                problemDetails = CreateProblemDetails(
                    status,
                    "Entity Not Found",
                    exception.Message,
                    "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4");
                break;
            
            case IncorrectPasswordException:
                status = StatusCodes.Status401Unauthorized;
                problemDetails = CreateProblemDetails(
                    status,
                    exception.Message,
                    "Wrong password was provided",
                    "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1");
                break;
            
            case PropertyNotUniqueException:
                status = StatusCodes.Status400BadRequest;
                problemDetails = CreateProblemDetails(
                    status,
                    "Provided propery was not unique",
                    exception.Message,
                    "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1");
                break;

            default:
                problemDetails = CreateProblemDetails(
                    status,
                    "Server Failure",
                    "An unexpected error occurred.",
                    "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1");
                break;
        }

        httpContext.Response.StatusCode = status;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static ProblemDetails CreateProblemDetails(
        int status,
        string title,
        string detail,
        string type = null)
    {
        return new ProblemDetails
        {
            Type = type,
            Status = status,
            Title = title,
            Detail = detail
        };
    }
}
