using System.Net;
using System.Text.Json;

namespace Api.Middleware
{
    /**
     * NOT IN USE!
     * **/
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occured");
                await HandleExceptionAsync(context, ex);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode;

            switch(exception)
            {
                //case KeyNotFoundException:
                //    statusCode = (int)HttpStatusCode.NotFound; // 404
                //    break;

                //case UnauthorizedAccessException:
                //    statusCode = (int)HttpStatusCode.Unauthorized; // 401
                //    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError; // 500
                    break;
            }

            var response = new
            {
                title = exception.Message,
                status = statusCode,
            };

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
