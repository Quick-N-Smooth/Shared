using Microsoft.AspNetCore.Http;
using System.Net;

namespace WebApi.Middlewares
{
    public class ExceptionHandlingServiceDummy : IExceptionHandlingService
    {
        public async Task HandleExceptionAsync(HttpContext context, RequestDelegate _next)
        {
            await _next(context);
        }
    }
}
