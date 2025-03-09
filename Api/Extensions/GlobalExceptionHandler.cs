using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions
{
    // Finish it later
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled exception occured.");

            var problemDetails = new ProblemDetails
            {
                Status = StatusCode(exception),
                Title = "Internal Server Error",
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Extensions = new Dictionary<string, object?>
                {
                    {"errors", new string[] {"test", } },
                    {"traceId", httpContext.TraceIdentifier},

                }

            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }


        private static int StatusCode(Exception exception)
        {
            var statusCode = exception switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound,
                _=> StatusCodes.Status500InternalServerError
            };

            return statusCode;
        }
    }
}
