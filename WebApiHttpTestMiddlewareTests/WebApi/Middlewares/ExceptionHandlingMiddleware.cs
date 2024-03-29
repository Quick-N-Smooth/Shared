﻿/* Sample for middleware and service has been taken from
    https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-6.0
*/

using Microsoft.Extensions.Options;
using WebApi.Services;

namespace WebApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IExceptionHandlingService _service;

    public ExceptionHandlingMiddleware(RequestDelegate next, IExceptionHandlingService service)
    {
        _next = next;
        _service = service;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _service.InvokeWithExceptionHandlingAsync(context, _next);
    }
}

public static class ExceptionHandlingMiddlewareExtentions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException("app");
        }

        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}