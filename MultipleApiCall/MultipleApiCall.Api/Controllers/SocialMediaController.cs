using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace CallAsyncMethodsParallel.Api.Controllers;

[ApiController]
public class SocialMediaController : ControllerBase
{

    [HttpGet("youtube200")]
    public async Task<IActionResult> GetYoutubeActionDetails(int delay)
    {
        var subsribers = new Collection<string>();
        for (var i = 0; i < 10; i++)
        {
            subsribers.Add("YoutubeSubscriber_" + i.ToString());
        }
        await Task.Delay(delay);
        return Ok(new
        {
            Subscribers = subsribers
        });
    }

    [HttpGet("github200")]
    public async Task<IActionResult> GetGitHubDetails(int delay)
    {
        var subsribers = new Collection<string>();
        for (var i = 0; i < 10; i++)
        {
            subsribers.Add("GithubSubscriber_" + i.ToString());
        }
        await Task.Delay(delay);
        return Ok(new
        {
            Subscribers = subsribers
        });
    }

    [HttpGet("twitter200")]
    public async Task<IActionResult> GetTwitterDetails(int delay)
    {
        var subsribers = new Collection<string>();
        for (var i = 0; i < 10; i++)
        {
            subsribers.Add("TwitterSubscriber_" + i.ToString());
        }
        await Task.Delay(delay);
        return Ok(new
        {
            Subscribers = subsribers
        });
    }

    [HttpGet("youtube401")]
    public async Task<IActionResult> GetYoutubeActionDetailsUnauthorized(int delay)
    {
        await Task.Delay(delay);
        return Unauthorized(new
        {
            Message = "This is a 401 error."
        });
    }

    [HttpGet("twitter400")]
    public async Task<IActionResult> GetTwitterDetailsReturnBadRequest(int delay)
    {
        await Task.Delay(delay);
        return BadRequest(new
        {
            Message = "This is a 400 error."
        });
    }

    [HttpGet("github500")]
    public async Task<IActionResult> GetGitHubDetailsThrowException(int delay)
    {
        await Task.Delay(delay);
        throw new TimeoutException("Exception from github call");
    }

    [HttpGet("twitter500")]
    public async Task<IActionResult> GetTwitterDetailsThrowException(int delay)
    {
        await Task.Delay(delay);
        throw new TimeoutException("This is an exception from twitter call");
    }
}
