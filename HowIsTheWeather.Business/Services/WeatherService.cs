using HowIsTheWeather.Service.Database;
using HowIsTheWeather.Service.Database.Entities;
using HowIsTheWeather.Service.Domain.Dtos.Responses;
using HowIsTheWeather.Service.Domain.Interfaces;
using HowIsTheWeather.Service.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Service.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IForecastWeatherApiClient _forecastWeatherApiClient;
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<WeatherService> _logger;
        public WeatherService(IForecastWeatherApiClient forecastWeatherApiClient, ILocationRepository locationRepository, ILogger<WeatherService> logger)
        {

            _forecastWeatherApiClient = forecastWeatherApiClient;
            _locationRepository = locationRepository;
            _logger = logger;
        }

        public async Task<WeatherResponseDto?> GetWeatherAsync(double latitude, double longitude)
        {
            WeatherResponseDto? result = null;
            try
            {
                result = await _forecastWeatherApiClient.GetWeatherAsync(latitude, longitude);
                if (result is not null)
                {

                    GeographicalLocationModel location = result.ToGeographicalLocationModel(latitude, longitude);
                    await _locationRepository.InsertAsync(location);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Request failed :{ex.Message}");
                result = await _locationRepository.GetLatestWeatherAsync(latitude, longitude);
            }

            return result;
        }
    }
}
