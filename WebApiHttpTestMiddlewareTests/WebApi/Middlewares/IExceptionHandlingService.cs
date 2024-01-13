namespace WebApi.Middlewares
{
    public interface IExceptionHandlingService
    {
        Task HandleExceptionAsync(HttpContext context, RequestDelegate _next);
    }
}