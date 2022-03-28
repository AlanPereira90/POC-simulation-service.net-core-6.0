using Microsoft.AspNetCore.Mvc;

namespace src.application.controllers;

[ApiController]
[Route("[controller]")]
public class ReadinessController : ControllerBase
{

  private readonly ILogger<ReadinessController> _logger;

  public ReadinessController(ILogger<ReadinessController> logger)
  {
    _logger = logger;
  }

  [HttpGet(Name = "/readiness")]
  public IActionResult Readiness()
  {
    return Ok();
  }
}
