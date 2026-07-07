namespace todoApi;

using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using todoApi.Application.Exceptions; // Reference your application exceptions

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IHostEnvironment env) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // 1. Log based on severity
        if (exception is DuplicateResourceException)
        {
            // Log as information/warning since it's just a user error, keeping your logs clean
            logger.LogWarning("Validation failure: {Message}", exception.Message);
        }
        else
        {
            // Log as a true error if the database or system actually crashed
            logger.LogError(exception, "An unhandled system exception occurred.");
        }

        // 2. Map Status Code and Messages dynamically
        var (statusCode, title, detail) = exception switch
        {
            // For known business exceptions, it is safe to show the message even in Production
            DuplicateResourceException => (
                (int)HttpStatusCode.Conflict,
                "Conflict",
                exception.Message
            ),

            // Catch-all for real system errors (DB connection drops, null references, etc.)
            _ => (
                (int)HttpStatusCode.InternalServerError,
                "Server Error",
                env.IsDevelopment() ? exception.Message : "An unexpected error occurred while processing your request."
            )
        };

        // 3. Build response
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
