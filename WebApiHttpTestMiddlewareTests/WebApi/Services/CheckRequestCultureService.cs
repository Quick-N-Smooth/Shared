using System.Net;

namespace WebApi.Services
{
    public class CheckRequestCultureService : ICheckRequestCultureService
    {
        public async Task CheckRequestCultureAsync(HttpContext context)
        {
            var cultureQuery = context.Request.Headers["culture"];

            if (string.IsNullOrWhiteSpace(cultureQuery))
            {
                throw new ArgumentNullException("Argument null exception from middleware");
            }
            return;
        }
    }

    public static class CheckRequestCultureServiceExtentions
    {
        public static IServiceCollection AddCheckRequestCultureService(this IServiceCollection services)
        {
            services.AddSingleton<ICheckRequestCultureService, CheckRequestCultureService>();
            return services;
        }
    }
}
