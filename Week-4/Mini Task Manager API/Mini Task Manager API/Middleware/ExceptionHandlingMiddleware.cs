using System.Text.Json;

namespace Mini_Task_Manager_API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                logger.LogError(ex, "an unhandled exception occured");
                
                context.Response.StatusCode = 500;

                context.Response.ContentType = "application/json";

                    var response = new
                    {
                        message = ex.Message
                };


                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}

