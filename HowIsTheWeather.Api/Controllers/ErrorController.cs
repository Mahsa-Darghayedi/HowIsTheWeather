using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HowIsTheWeather.Api.Controllers;


public class ErrorController : ControllerBase
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception == null)
            return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An error has occurred.");

        _logger.LogError($"An error has occurred. Message {exception.Message}");
        return Problem(statusCode: StatusCodes.Status500InternalServerError, title: exception.Message);
    }

}
