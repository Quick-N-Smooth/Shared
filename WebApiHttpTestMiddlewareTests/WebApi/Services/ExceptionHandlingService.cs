using Microsoft.AspNetCore.Connections;
using System;
using System.Net;

namespace WebApi.Services;

public class ExceptionHandlingService : IExceptionHandlingService
{

    public async Task InvokeWithExceptionHandlingAsync(HttpContext context, RequestDelegate _next)
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

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ExceptionResponse response = CreateExceptionResponse(exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }

    // NOTE the internal access modifier. Test project can see it 
    internal ExceptionResponse CreateExceptionResponse(Exception exception)
    {
        ExceptionResponse exceptionResponse = exception switch
        {
            ArgumentNullException _ => new ExceptionResponse(HttpStatusCode.BadRequest, $"ArgumentNullException handled in middleware. {exception.Message}"),
            ConnectionResetException _ => HandleConnectionResetException(exception),
            _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error handled in middleware.")
        };
        ExceptionResponse response = exceptionResponse;
        return response;
    }

    // handle a specific exception in a separate method
    private static ExceptionResponse HandleConnectionResetException(Exception exception)
    {
        return new ExceptionResponse(HttpStatusCode.RequestTimeout, $"ConnectionResetException handled in middleware. {exception.Message}");
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
