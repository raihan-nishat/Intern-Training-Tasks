using System.Text.Json;

namespace Mini_Task_Manager_API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }

            catch (Exception)
            {
                context.Response.StatusCode = 500;

                context.Response.ContentType = "application/json";

                var response = new
                {
                    message = "something went wrong on the server"
                };


                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}

