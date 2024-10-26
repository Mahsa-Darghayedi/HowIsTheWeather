using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Service.Domain.Dtos.Responses;

public class WeatherResponseDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double GenerationTime_ms { get; set; }
    public int Utc_Offset_Seconds { get; set; }
    public string Timezone { get; set; } = string.Empty;
    public string Timezone_Abbreviation { get; set; } = string.Empty;
    public double Elevation { get; set; }
    public HourlyDataPointDto Hourly_Units { get; set; }
    public HourlyDataDto Hourly { get; set; }

}


public record HourlyDataDto
{
    public string[] Time { get; set; } = [];
    public decimal[] Temperature_2m { get; set; } = [];
}

public record HourlyDataPointDto
{
    public string Time { get; set; }
    public string Temperature_2m { get; set; }
}