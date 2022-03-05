using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApi5.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            string message = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " [Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
            Console.WriteLine(message);
            await _next(context);
            watch.Stop();
            message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + 
            context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + "ms";
            Console.WriteLine(message);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}