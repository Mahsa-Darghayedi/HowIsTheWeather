using HowIsTheWeather.Service.Database;
using HowIsTheWeather.Service.Domain.Interfaces;
using HowIsTheWeather.Service.ExternalServices;
using HowIsTheWeather.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace HowIsTheWeather.Service.Extensions;

public static class ServiceDI
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddDbContext<HowIsTheWeatherDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("HowIsTheWeatherDb")));

        services.AddScoped<IWeatherService, WeatherService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ILocationRepository, LocationRepository>();

        services.AddHttpClient<IForecastWeatherApiClient, ForecastWeatherApiClient>()
                 .SetHandlerLifetime(TimeSpan.FromMinutes(2))
                 .AddDefaultLogger()
                 .AddPolicyHandler(GetRetryPolicy())
                 .ConfigureHttpClient(c=> c.Timeout = TimeSpan.FromSeconds(4));

     
        return services;
    }

    private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
        => HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .Or<HttpRequestException>()
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}
