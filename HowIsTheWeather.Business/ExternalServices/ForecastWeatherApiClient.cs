using HowIsTheWeather.Service.Domain.Dtos.Responses;
using HowIsTheWeather.Service.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Extensions.Http;
using System.Net;

namespace HowIsTheWeather.Service.ExternalServices;
public class ForecastWeatherApiClient : IForecastWeatherApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ForecastWeatherApiClient> _logger;
    public ForecastWeatherApiClient(HttpClient httpClient, ILogger<ForecastWeatherApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<WeatherResponseDto?> GetWeatherAsync(double latitude, double longitude)
    {
        var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m";
        HttpResponseMessage response = await _httpClient.GetAsync(url);
       
        //return a bad request on purpose 
        //response.StatusCode = HttpStatusCode.BadRequest;


        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<WeatherResponseDto>(json);
    }
}
