using HowIsTheWeather.Service.Database.Entities;
using HowIsTheWeather.Service.Domain.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Service.Extensions;

internal static class Mapper
{

    public static WeatherResponseDto ToWeatherResponseDto(this GeographicalLocationModel model)
        => new()
        {
            Latitude = model.Latitude,
            Longitude = model.Longitude,
            GenerationTime_ms = model.GenerationTime_ms,
            Utc_Offset_Seconds = model.Utc_Offset_Seconds,
            Timezone = model.Timezone,
            Timezone_Abbreviation = model.Timezone_Abbreviation,
            Elevation = model.Elevation,
            Hourly_Units = new HourlyDataPointDto() { Time = model.Time_Unit, Temperature_2m = model.Temperature_2m_Unit },
            Hourly = new HourlyDataDto()
            {
                Time = model.Temperature.Select(c => c.Time.GetTimeStr()).ToArray(),
                Temperature_2m = model.Temperature.Select(c => c.Temperature).ToArray()
            }
        };
    public static GeographicalLocationModel ToGeographicalLocationModel(this WeatherResponseDto dto)
        => new(dto.Latitude, dto.Longitude)
        {
            GenerationTime_ms = dto.GenerationTime_ms,
            Utc_Offset_Seconds = dto.Utc_Offset_Seconds,
            Timezone = dto.Timezone,
            Timezone_Abbreviation = dto.Timezone_Abbreviation,
            Elevation = dto.Elevation,
            Time_Unit = dto.Hourly_Units.Time,
            Temperature_2m_Unit = dto.Hourly_Units.Temperature_2m,
            Temperature = dto.Hourly.Time.Zip(dto.Hourly.Temperature_2m, (t, m) => new TemperatureModel(t.GetDateTime(), m))
                                              .ToList()
        };
    public static GeographicalLocationModel ToGeographicalLocationModel(this WeatherResponseDto dto, double latitude, double longitude)
        => new(latitude, longitude)
        {
            GenerationTime_ms = dto.GenerationTime_ms,
            Utc_Offset_Seconds = dto.Utc_Offset_Seconds,
            Timezone = dto.Timezone,
            Timezone_Abbreviation = dto.Timezone_Abbreviation,
            Elevation = dto.Elevation,
            Time_Unit = dto.Hourly_Units.Time,
            Temperature_2m_Unit = dto.Hourly_Units.Temperature_2m,
            Temperature = dto.Hourly.Time.Zip(dto.Hourly.Temperature_2m, (t, m) => new TemperatureModel(t.GetDateTime(), m))
                                              .ToList()
        };


}
