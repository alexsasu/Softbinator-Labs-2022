using slabs_project.Helpers;
using System.Net;
using System.Text.Json;

namespace slabs_project.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<ExceptionMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                logger.LogError("Message: " + ex.Message + "Message: " + ex.InnerException, "Exception middleware caught an exception.");

                var response = context.Response;
                response.ContentType = "application/json";

                switch (ex)
                {
                    case AppException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case UnauthorizedAccessException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case KeyNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case NotImplementedException:
                        response.StatusCode = (int)HttpStatusCode.NotImplemented;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        logger.LogCritical("Message: " + ex.Message + "Message: " + ex.InnerException, "Unexpected error caught by exception middleware.");
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = ex?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
