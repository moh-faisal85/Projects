using System.Net;
using System.Text;

namespace Training.DotNetCore.Project.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(
            ILogger<ExceptionHandlerMiddleware> logger,
            RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var errorId = Guid.NewGuid();

            context.Request.EnableBuffering();

            string body = "";
            if (context.Request.ContentLength > 0)
            {
                context.Request.Body.Position = 0;
                using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            var headers = context.Request.Headers
                .ToDictionary(h => h.Key, h => h.Value.ToString());

            var fullUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";

            var log = new
            {
                Timestamp = DateTime.UtcNow,
                ErrorId = errorId,
                ExceptionMessage = ex.Message,
                ExceptionType = ex.GetType().FullName,
                //StackTrace = ex.StackTrace,
                Source = ex.Source,
                Request = new
                {
                    FullUrl = fullUrl,
                    Method = context.Request.Method,
                    Endpoint = context.Request.Path,
                    QueryString = context.Request.QueryString.Value,
                    Headers = headers,
                    Body = body,
                },
                User = context.User?.Identity?.Name ?? "anonymous"
            };
            /*
             Exception: ex
             MessageTemplate = "Unhandled Exception: {@ExceptionLog}"
             Level = LogError = Error
             */
            _logger.LogError(ex, "Unhandled Exception: {@ExceptionLog}", log);

            // === Return unified JSON response ===
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                Id = errorId,
                Message = "Something went wrong. Please use the ErrorId for support."
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }

    /// <summary>
    /// Not used initiate Practice Code
    /// </summary>
    public class ExceptionHandlerMiddlewareTemp
    {
        private readonly ILogger<ExceptionHandlerMiddlewareTemp> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddlewareTemp(
            ILogger<ExceptionHandlerMiddlewareTemp> logger,
            RequestDelegate next
            )
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);

            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                var logObject = new
                {
                    Timestamp = DateTime.UtcNow,
                    Level = "Error",
                    ErrorId = errorId,
                    Message = ex.Message,
                    ExceptionType = ex.GetType().FullName,
                    Path = httpContext.Request.Path,
                    QueryString = httpContext.Request.QueryString.ToString(),
                    HttpMethod = httpContext.Request.Method,
                    User = httpContext.User?.Identity?.Name ?? "anonymous",
                    Source = ex.Source,
                    StackTrace = ex.StackTrace
                };

                logger.LogError("{@LogDetails}", logObject);

                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong! We are working on it."
                });
            }

            //catch (Exception ex)
            //{
            //    var errorId = Guid.NewGuid();
            //    //Log This Exception
            //    logger.LogError(ex, $"{errorId} : {ex.Message}");

            //    //Return Custom Error Response
            //    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //    httpContext.Response.ContentType = "application/json";
            //    var error = new
            //    {
            //        Id = errorId,
            //        ErrorMessage = "Something Went Wrong! We are looking into resolving this."
            //    };
            //    await httpContext.Response.WriteAsJsonAsync(error);
            //}
        }
    }
}

