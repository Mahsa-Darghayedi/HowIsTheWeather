using FluentAssertions;
using HowIsTheWeather.Service.Domain.Dtos.Responses;
using HowIsTheWeather.Service.Domain.Interfaces;
using HowIsTheWeather.Service.ExternalServices;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HowIsTheWeather.Test;

public class ForecastWeatherApiClient_Test
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private HttpClient _httpClient;
    private  IForecastWeatherApiClient _forecastWeatherApiClient;
    private readonly Mock<ILogger<ForecastWeatherApiClient>> _loggerMock;
    public ForecastWeatherApiClient_Test()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
         _loggerMock = new();
    }

    [Fact]
    public async Task GetWeatherAsync_ShouldBeFailed_RequestException()
    {
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException());

        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _forecastWeatherApiClient = new ForecastWeatherApiClient(_httpClient, _loggerMock.Object);

        var latitude = 52.52;
        var longitude = 13.41;

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => _forecastWeatherApiClient.GetWeatherAsync(latitude, longitude));

        exception.Should().NotBeNull();
        exception.Should().BeOfType<HttpRequestException>();
    }


    [Fact]
    public async Task GetWeatherAsync_ShouldBeSuccessful()
    {
        _httpClient = new HttpClient();
        _forecastWeatherApiClient = new ForecastWeatherApiClient(_httpClient, _loggerMock.Object);

        var latitude = 52.52;
        var longitude = 13.41;

        var result = await _forecastWeatherApiClient.GetWeatherAsync(latitude, longitude);

        result.Should().NotBeNull();
    }
}
