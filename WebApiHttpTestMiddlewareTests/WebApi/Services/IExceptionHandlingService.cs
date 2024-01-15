namespace WebApi.Services
{
    public interface IExceptionHandlingService
    {
        Task InvokeWithExceptionHandlingAsync(HttpContext context, RequestDelegate _next);
    }
}