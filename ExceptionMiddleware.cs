using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace SetResponseHeaders
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            _next = next;
            _env = env;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception error)
            {
                await HandleException(httpContext, error);
            }
        }

        private static async Task HandleException(HttpContext context, Exception error)
        {
            context.Response.ContentType = "application/json";
            var problemDetails = new ProblemDetails
            {
                Status = 400,
                Title = "Error",
                Detail = error.Message
            };
            context.Response.StatusCode = (int)problemDetails.Status;
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var jsonResult = JsonSerializer.Serialize(problemDetails, options);
            await context.Response.WriteAsync(jsonResult);
        }
    }
}