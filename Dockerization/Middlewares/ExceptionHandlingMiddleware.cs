using System.Net;
using System.Text.Json;

namespace Dockerization.Middlewares

{
    public class ExceptionHandlingMiddleware
    {
        public RequestDelegate _requestDelegate;
        public readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }

        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            _logger.LogError(ex.ToString());

            var errorMessage =
                new
                {
                    ex.Message,
                    Code = "system_error",
                };

            var customResponse = JsonSerializer.Serialize(errorMessage);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(customResponse);
        }
    }
}
