using Microsoft.AspNetCore.Mvc;

namespace src.application.controllers;

[ApiController]
[Route("[controller]")]
public class CreateSimulationController : ControllerBase
{
  private readonly ILogger<ReadinessController> _logger;

  public CreateSimulationController(ILogger<ReadinessController> logger)
  {
    _logger = logger;
  }

  [HttpPost(Name = "/simulations")]
  public IActionResult CreateSimulation()
  {
    return Accepted();
  }

}