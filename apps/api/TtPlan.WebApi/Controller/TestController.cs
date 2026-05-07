using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace TtPlan.WebApi.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class TestController: ControllerBase
{
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok($"From backend with love ${DateTime.UtcNow.ToString(CultureInfo.CurrentCulture)}");
    }
}