using Azure;
using Castle.Core.Logging;
using FluentAssertions;
using HowIsTheWeather.Service.Database;
using HowIsTheWeather.Service.Database.Entities;
using HowIsTheWeather.Service.Domain.Dtos.Responses;
using HowIsTheWeather.Service.Domain.Interfaces;
using HowIsTheWeather.Service.Extensions;
using HowIsTheWeather.Service.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Test;

public class WeatherService_Test
{
    private Mock<IForecastWeatherApiClient> _weatherApiClientMock;
    private Mock<ILocationRepository> _locationRepositoryMock;
    private readonly Mock<ILogger<WeatherService>> _loggerMock;
    private IWeatherService _weatherService;
    public WeatherService_Test()
    {
        _weatherApiClientMock = new();
        _locationRepositoryMock = new();
        _loggerMock = new();
    }
    [Fact]
    public async Task GetWeatherAsync_ResultShouldBeNull_WebServiceException_EmptyDatabase()
    {
        _weatherApiClientMock.Setup(c => c.GetWeatherAsync(It.IsAny<double>(), It.IsAny<double>())).ThrowsAsync(new Exception());
        _locationRepositoryMock.Setup(c => c.GetLatestWeatherAsync(It.IsAny<double>(), It.IsAny<double>())).Returns(Task.FromResult<WeatherResponseDto>(null));

        _weatherService = new WeatherService(_weatherApiClientMock.Object, _locationRepositoryMock.Object, _loggerMock.Object);

        var result = await _weatherService.GetWeatherAsync(It.IsAny<double>(), It.IsAny<double>());

        result.Should().BeNull();
    }


    [Fact]
    public async Task GetWeatherAsync_ResultReturnedSuccessfullyFromWebService()
    {

        var response = GetSampleDto();

        _weatherApiClientMock.Setup(c => c.GetWeatherAsync(It.IsAny<double>(), It.IsAny<double>())).Returns(Task.FromResult(response));
        var model = response?.ToGeographicalLocationModel();
        _locationRepositoryMock.Setup(c => c.InsertAsync(model)).Returns(Task.CompletedTask);
        _weatherService = new WeatherService(_weatherApiClientMock.Object, _locationRepositoryMock.Object, _loggerMock.Object);
        var result = await _weatherService.GetWeatherAsync(It.IsAny<double>(), It.IsAny<double>());
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(response);
        _locationRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<GeographicalLocationModel>()), Times.Once);
    }


    [Fact]
    public async Task GetWeatherAsync_ResultMustReturnFromDatabase_WebServiceException()
    {
        var response = GetSampleDto();
        var model = response?.ToGeographicalLocationModel();


        _weatherApiClientMock.Setup(c => c.GetWeatherAsync(It.IsAny<double>(), It.IsAny<double>())).ThrowsAsync(new Exception());
        _locationRepositoryMock.Setup(c => c.GetLatestWeatherAsync(It.IsAny<double>(), It.IsAny<double>())).Returns(Task.FromResult<WeatherResponseDto>(model.ToWeatherResponseDto()));
        _weatherService = new WeatherService(_weatherApiClientMock.Object, _locationRepositoryMock.Object, _loggerMock.Object);
        var result = await _weatherService.GetWeatherAsync(It.IsAny<double>(), It.IsAny<double>());
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(response);
    }

    private WeatherResponseDto GetSampleDto()
    {
        using StreamReader r = new("../../../forecast.json");
        string json = r.ReadToEnd();
        return JsonConvert.DeserializeObject<WeatherResponseDto>(json);
    }
}
