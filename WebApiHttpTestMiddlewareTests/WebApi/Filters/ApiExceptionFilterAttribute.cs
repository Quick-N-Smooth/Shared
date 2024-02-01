using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Omocom.BackOffice.Api.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
	private readonly IDictionary<Type, Action<ExceptionContext>> exceptionHandlers;
    private readonly bool handleUnknowException;

    public ApiExceptionFilterAttribute(bool handleUnknowException = true)
    {

		//this.logger = logger;
        // Register known exception types and handlers.
        exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ArgumentNullException), HandleArgumentNullException }
        };
        this.handleUnknowException = handleUnknowException;
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
            Title = "ArgumentNullException handled in FilterAttribute",
            Detail = $"{exception.Message}"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };

        context.ExceptionHandled = true;

    }

    private void HandleUnknownException(ExceptionContext context)
    {
        if (!handleUnknowException)
        {
            return;
        }

        var details = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Unknown exception handled in FilterAttribute",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Detail = "An error occurred while processing your request."
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;

	}
}
