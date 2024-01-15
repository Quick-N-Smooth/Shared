namespace WebApi.Services
{
    public interface ICheckRequestCultureService
    {
        Task CheckRequestCultureAsync(HttpContext context);
    }
}