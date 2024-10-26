using HowIsTheWeather.Service.Domain.Interfaces;
using HowIsTheWeather.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HowIsTheWeather.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{

    private readonly IWeatherService _weatherService;
    private readonly ILogger<WeatherController> _logger;

    public WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    [HttpGet(Name = "GetWeather")]
    public async Task<ActionResult> Get(double latitude, double longitude)
    {
        if (!latitude.IsLatitudeInRange() || !longitude.IsLongitudeInRange())
            return BadRequest();

        var result = await _weatherService.GetWeatherAsync(latitude, longitude).ConfigureAwait(false);
        return result is null ? NoContent() : Ok(result);
    }
}