using WebApi.Services;

namespace WebApi.Middlewares;

public class CheckRequestCultureMiddleware
{
    private readonly RequestDelegate _next;
    private readonly RequestDelegate _previous;
    private readonly ICheckRequestCultureService _service;

    public CheckRequestCultureMiddleware(RequestDelegate next, ICheckRequestCultureService service)
    {
        _next = next;
        _service = service;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _service.CheckRequestCultureAsync(context);
        await _next(context);
    }
}

public static class CheckRequestCultureMiddlewareExtentions
{
    public static IApplicationBuilder UseCheckRequestCultureMiddleware(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException("app");
        }

        return app.UseMiddleware<CheckRequestCultureMiddleware>();
    }
}
