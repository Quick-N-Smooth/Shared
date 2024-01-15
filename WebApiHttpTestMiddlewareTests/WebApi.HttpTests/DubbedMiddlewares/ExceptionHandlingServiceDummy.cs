using Microsoft.AspNetCore.Http;
using System.Net;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class ExceptionHandlingServiceDummy : IExceptionHandlingService
    {
        public async Task InvokeWithExceptionHandlingAsync(HttpContext context, RequestDelegate _next)
        {
            await _next(context);
        }
    }
}
