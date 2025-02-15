using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Controller]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("OK");
    }

    [HttpGet]
    public IActionResult Time()
    {
        return Content($"{DateTime.Now}\r\n{DateTime.UtcNow}\r\n{DateTimeOffset.Now.Offset}");
    }

    [HttpGet]
    public IActionResult MyIP()
    {
        return Content($"{HttpContext.Connection?.RemoteIpAddress}");
    }
}
