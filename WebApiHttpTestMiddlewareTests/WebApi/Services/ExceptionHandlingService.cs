using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            ArgumentNullException _ => HandleArgumentNullException(exception),
            ConnectionResetException _ => HandleConnectionResetException(exception),
            _ => HandleUnhandledException(exception)
        };
        ExceptionResponse response = exceptionResponse;
        return response;
    }

    private static ExceptionResponse HandleArgumentNullException(Exception exception)
    {
        var typedException = (ArgumentException)exception;

        var details = new ProblemDetails()
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "ArgumentNullException handled in Middleware",
            Detail = $"Parameter: {typedException.ParamName}; Message: {typedException.Message}",
        };

        // logger.Warning(context.Exception, "Client disconected, returning Timeout");

        return new ExceptionResponse(HttpStatusCode.BadRequest, details);
    }

    private static ExceptionResponse HandleConnectionResetException(Exception exception)
    {
        var details = new ProblemDetails()
        {
            Type = typeof(Exception).FullName,
            Title = "ConnectionResetException handled in Middleware",
            Detail =  $"{typeof(Exception).Name} - {exception.Message}"
        };

        // logger.Warning(context.Exception, "Client disconected, returning Timeout");

        return new ExceptionResponse(HttpStatusCode.RequestTimeout, details);
    }

    private static ExceptionResponse HandleUnhandledException(Exception exception)
    {

        // NOTE: the details of the exception can be hidden from the response.
        // it can be Logged though
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Detail = null // or exception.Message
        };

        return new ExceptionResponse(HttpStatusCode.InternalServerError, details);
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

public record ExceptionResponse(HttpStatusCode StatusCode, ProblemDetails Description);
