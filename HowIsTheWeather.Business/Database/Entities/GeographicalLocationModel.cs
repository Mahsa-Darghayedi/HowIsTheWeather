using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Service.Database.Entities;

public class GeographicalLocationModel
{
    public int Id { get; set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public required double GenerationTime_ms { get; init; }
    public required int Utc_Offset_Seconds { get; init; }
    public required string Timezone { get; init; }
    public required string Timezone_Abbreviation { get; init; }
    public required double Elevation { get; init; }
    public required string Time_Unit { get; init; }
    public required string Temperature_2m_Unit { get; init; }


    public virtual ICollection<TemperatureModel> Temperature { get; set; } = [];


    protected GeographicalLocationModel()
    {
        // Just 4 Ef
    }


    public GeographicalLocationModel(double latitude, double loungitude)
    {
        //error handeling
        Latitude = latitude;
        Longitude = loungitude;
    }


    public void SetTemperature(List<TemperatureModel> temperatureModels)
        => Temperature = temperatureModels;

}

