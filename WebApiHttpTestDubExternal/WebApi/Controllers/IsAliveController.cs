using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IsAliveController : ControllerBase
    {
        [HttpGet]
        public IsAliveViewModel Get()
        {
            return new IsAliveViewModel();
        }
    }
}
