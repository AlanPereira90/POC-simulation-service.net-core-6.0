using Microsoft.AspNetCore.Mvc;
using src.domain.simulation.interfaces;
using src.domain.simulation.dtos;
using src.application.controllers.simulation.requests;

namespace src.application.controllers;

[ApiController]
[Route("/simulations")]
public class SimulationController : ControllerBase
{
  private readonly ILogger<ReadinessController> _logger;
  private readonly ISimulationService _simulationService;

  public SimulationController(ILogger<ReadinessController> logger, ISimulationService simulationService)
  {
    _logger = logger;
    _simulationService = simulationService;
  }

  [HttpPost]
  public IActionResult CreateSimulation(
    [FromBody] CreateSimulationRequest request,
    [FromHeader(Name = "x-user-id")] string userId
  )
  {
    var dto = SimulationDTO.FromApplication(request, userId);
    var id = _simulationService.Create(dto);
    return Accepted($"/simulations/{id}");
  }

  [HttpGet]
  [Route("{id}")]
  public IActionResult GetSimulation(
    [FromRoute] string id,
    [FromHeader(Name = "x-user-id")] string userId
  )
  {
    var simulation = _simulationService.Retrieve(Guid.Parse(id), userId);
    return Ok(simulation);
  }

  [HttpPatch]
  [Route("{id}/cancel")]
  public IActionResult CancelSimulation(
    [FromRoute] string id,
    [FromHeader(Name = "x-user-id")] string userId
  )
  {
    var simulationId = _simulationService.Cancel(Guid.Parse(id), userId);
    return Accepted($"/simulations/{simulationId}");
  }

  [HttpPatch]
  [Route("{id}/propose")]
  public IActionResult ProposeSimulation(
    [FromRoute] string id,
    [FromHeader(Name = "x-user-id")] string userId
  )
  {
    var simulationId = _simulationService.Propose(Guid.Parse(id), userId);
    return Accepted($"/simulations/{simulationId}");
  }

  [HttpPatch]
  [Route("{id}/accept")]
  public IActionResult AcceptSimulation(
    [FromRoute] string id,
    [FromHeader(Name = "x-user-id")] string userId
  )
  {
    var simulationId = _simulationService.Finish(Guid.Parse(id), userId);
    return Accepted($"/simulations/{simulationId}");
  }

}