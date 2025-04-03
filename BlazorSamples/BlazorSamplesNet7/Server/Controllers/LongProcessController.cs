using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BlazorTemplate.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class LongProcessController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public LongProcessController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<string> Get(CancellationToken cancellationToken)
    {
        //cancellationToken.ThrowIfCancellationRequested();

        var resultBuilder = new StringBuilder();
        for (var i = 1; i <= 10; i++)
        {
            resultBuilder.Append(i.ToString() + ", ");
            await Task.Delay(1000, cancellationToken);
        }
                
        return resultBuilder.ToString();
    }
}