using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Service.Database.Entities;

public class TemperatureModel
{
    public int Id { get; set; }
    public int LocationId { get; private set; }
    public DateTime Time { get; private set; }
    public decimal Temperature { get; private set; }

    public GeographicalLocationModel GeographicalLocation { get; set; }



    protected TemperatureModel()
    {
        //just 4 ef
    }

    public TemperatureModel(DateTime time, decimal temperature)
    {
        Time = time;
        Temperature = Math.Round(temperature,1);
    }
}
