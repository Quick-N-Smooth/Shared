using Microsoft.AspNetCore.Mvc;

namespace CallAsyncMethodsParallel.Api.Controllers
{
    [ApiController]
    public class SocialMediaController : ControllerBase
    {

        [HttpGet("youtube200")]
        public async Task<IActionResult> GetYoutubeActionDetails()
        {
            await Task.Delay(1000);
            return Ok(new
            {
                Subscribers = 1450
            });
        }

        [HttpGet("youtube401")]
        public async Task<IActionResult> GetYoutubeActionDetailsUnauthorized()
        {
            await Task.Delay(2000);
            return Unauthorized(new
                {
                    Message = "This is a 401 error."
                });
        }

        [HttpGet("github200")]
        public async Task<IActionResult> GetGitHubDetails()
        {
            await Task.Delay(1000);
            return Ok(new
            {
                Followers = 450
            }) ;
        }

        [HttpGet("github500")]
        public async Task<IActionResult> GetGitHubDetailsThrowException()
        {
            await Task.Delay(2000);
            throw new Exception("Exception from github call");
        }

        [HttpGet("twitter200")]
        public async Task<IActionResult> GetTwitterDetails()
        { 
            await Task.Delay (1000);
            return Ok(new
            {
                Followers = 15
            });
        }

        [HttpGet("twitter500")]
        public async Task<IActionResult> GetTwitterDetailsThrowException()
        {
            await Task.Delay(2000);
            throw new Exception("This is an exception from twitter call");
        }

        [HttpGet("twitter400")]
        public async Task<IActionResult> GetTwitterDetailsReturnBadRequest()
        {
            await Task.Delay(2000);
            return BadRequest(new
            {
                Message = "This is a 400 error."
            });
        }
    }
}
