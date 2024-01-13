using System;
using System.Net;

namespace WebApi.Middlewares;

public class ExceptionHandlingService : IExceptionHandlingService
{

    public async Task HandleExceptionAsync(HttpContext context, RequestDelegate _next)
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

    public async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ExceptionResponse response = exception switch
        {
            ArgumentNullException _ => new ExceptionResponse(HttpStatusCode.BadRequest, $"ArgumentNullException handled in middleware. {exception.Message}"),
            _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error handled in middleware.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}

public static class CheckRequExtentions
{
    public static IServiceCollection AddExceptionHandlingService(this IServiceCollection services)
    {
        services.AddSingleton<IExceptionHandlingService, ExceptionHandlingService>();
        return services;
    }
}

public record ExceptionResponse(HttpStatusCode StatusCode, string Description);
