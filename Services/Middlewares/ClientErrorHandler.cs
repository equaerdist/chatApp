using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApplication5.Services.Middlewares
{
    public class ClientErrorHandler
    {
        private readonly RequestDelegate _next;

        public ClientErrorHandler(RequestDelegate next) 
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            if(context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                var problem = new ValidationProblemDetails()
                {
                    Errors = { new KeyValuePair<string, string[]>("Resource", new[] { "Доступ к ресурсу ограничен" }) },
                    Status = (int)HttpStatusCode.Forbidden,
                    Detail = "Read more in info",
                    Title = "Access error"
                };
                await context.Response.WriteAsJsonAsync(problem);
            }
        }

    }
}
