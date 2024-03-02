using Microsoft.AspNetCore.Mvc;

namespace AutoRainAPI.Controllers;

[ApiController]
public class HealthCheck : ControllerBase
{
    [HttpGet("health-check")]
    public async Task<IActionResult> GetHealth() => Ok(); 
}