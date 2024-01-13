using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Middlewares;

namespace Omocom.BackOffice.Api.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
	private readonly IDictionary<Type, Action<ExceptionContext>> exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
		//this.logger = logger;
        // Register known exception types and handlers.
        exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ArgumentNullException), HandleArgumentNullException }
        };
    }

	public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

	private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (exceptionHandlers.ContainsKey(type))
        {
            exceptionHandlers[type].Invoke(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleArgumentNullException(ExceptionContext context)
    {
        var exception = (ArgumentNullException)context.Exception;

        var details = new ProblemDetails()
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = $"{exception.Message}",
            Detail = "ArgumentNullException handled in FilterAttribute"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };

        context.ExceptionHandled = true;

    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Detail = "Unknown exception handled in FilterAttribute"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;

	}
}
