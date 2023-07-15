using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace WebApplication5.Services.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger) { _next = next; _logger = logger; }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ProblemDetails error = new()
                {
                    Detail = "Something problems with service",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Error occured",
                    Type = "Error"
                };
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
