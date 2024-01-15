using Microsoft.AspNetCore.Http;
using System.Net;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CheckRequestCultureServiceDummy : ICheckRequestCultureService
    {
        public async Task CheckRequestCultureAsync(HttpContext context)
        {
            return;
        }
    }
}
