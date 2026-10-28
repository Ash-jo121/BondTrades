using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails statusDetails = exception switch
        {
            ConflictException => new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Type = exception.GetType().ToString(),
                Detail = "conflict exception"
            },
            NotFoundException => new ProblemDetails { Status = StatusCodes.Status404NotFound, Type = exception.GetType().ToString(), Detail = "not found exception" },
            BusinessRuleException => new ProblemDetails { Status = StatusCodes.Status422UnprocessableEntity, Type = exception.GetType().ToString(), Detail = "business rule exception" },
            _ => new ProblemDetails { Status = StatusCodes.Status500InternalServerError, Type = exception.GetType().ToString(), Detail = "other exception" }
        };

        httpContext.Response.StatusCode = statusDetails.Status ?? StatusCodes.Status500InternalServerError;

        // 3. (Optional) Write a response body, such as a ProblemDetails object
        await httpContext.Response.WriteAsJsonAsync(statusDetails, cancellationToken);

        // 4. Return true to signal that the exception has been handled
        return true;
    }

}