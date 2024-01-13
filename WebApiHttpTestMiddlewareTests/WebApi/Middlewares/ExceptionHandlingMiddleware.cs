/* Sample for middleware and service has been taken from
    https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-6.0
*/

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
        await _service.HandleExceptionAsync(context, _next);
    }
}
