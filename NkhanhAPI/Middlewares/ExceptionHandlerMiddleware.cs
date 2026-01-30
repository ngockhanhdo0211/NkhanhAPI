using System.Net;
using System.Text.Json;

namespace NkhanhAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // 🔥 Log đầy đủ ngữ cảnh
                logger.LogError(
                    ex,
                    "Exception occurred | Method: {Method} | Path: {Path}",
                    context.Request.Method,
                    context.Request.Path
                );

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    statusCode = context.Response.StatusCode,
                    message = "Đã xảy ra lỗi hệ thống. Vui lòng thử lại sau."
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response)
                );
            }
        }
    }
}
