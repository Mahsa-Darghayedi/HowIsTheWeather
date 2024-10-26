using HowIsTheWeather.Service.Database.Entities;
using HowIsTheWeather.Service.Domain.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Service.Database;

public interface ILocationRepository
{
    Task<WeatherResponseDto?> GetLatestWeatherAsync(double latitude, double longitude);
    Task InsertAsync(GeographicalLocationModel model);
}
