using HxcCommon;

namespace HxcApi.ExceptionHandling.Middleware;

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class KnownExceptionMiddleware(RequestDelegate next, ILogger<KnownExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (ex is KnownException knownException)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"Bad Request: {knownException.Message}");
            }
            else
            {
                throw;
            }
        }
    }
}

public static class KnownExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseKnownExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<KnownExceptionMiddleware>();
    }
}
