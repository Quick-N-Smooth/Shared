using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        [HttpGet("Home")]
        public async Task<IActionResult> Home()
        {
            return Ok(new
            {
                Application = "Web API http server."
            }
            );
        }
    }
}
