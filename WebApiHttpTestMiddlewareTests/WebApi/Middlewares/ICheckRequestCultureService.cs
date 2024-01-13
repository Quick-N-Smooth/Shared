
namespace WebApi.Middlewares
{
    public interface ICheckRequestCultureService
    {
        Task CheckRequestCultureAsync(HttpContext context);
    }
}