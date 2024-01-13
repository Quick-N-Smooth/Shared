using Microsoft.AspNetCore.Mvc;

namespace HttpServer.Controllers
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
                Application = "Authors http server."
            }
            );
        }
    }
}
