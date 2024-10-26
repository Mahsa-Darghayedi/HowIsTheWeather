using HowIsTheWeather.Service.Domain.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Service.Domain.Interfaces;

public interface IForecastWeatherApiClient
{
    Task<WeatherResponseDto?> GetWeatherAsync(double latitude, double longitude);
}
    