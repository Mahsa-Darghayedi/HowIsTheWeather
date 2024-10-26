using HowIsTheWeather.Service.Database.Entities;
using HowIsTheWeather.Service.Domain.Dtos.Responses;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Service.Extensions;

public static class Converter
{
    public static DateTime GetDateTime(this string value)
    {

        if (DateTime.TryParse(value, out DateTime dateTime))
            return dateTime;

        throw new Exception();
    }
    public static string GetTimeStr(this DateTime value)
    => value.ToString("yyyy-MM-dd'T'HH:mm");


    public static bool IsLatitudeInRange(this double value)
         => Math.Abs(value) <= 90;


    public static bool IsLongitudeInRange(this double value)
       => Math.Abs(value) <= 180;
}

