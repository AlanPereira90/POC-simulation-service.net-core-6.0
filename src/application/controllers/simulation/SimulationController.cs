using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

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
  public async Task<IActionResult> CreateSimulation(
    [FromBody] CreateSimulationRequest request,
    [FromHeader(Name = "x-user-id")][Required] string userId
  )
  {
    var dto = SimulationDTO.FromApplication(request, userId);
    var id = await _simulationService.Create(dto);
    return Accepted($"/simulations/{id}");
  }

  [HttpGet]
  [Route("{id}")]
  public async Task<IActionResult> GetSimulation(
    [FromRoute] string id,
    [FromHeader(Name = "x-user-id")][Required] string userId
  )
  {
    var simulation = await _simulationService.Retrieve(Guid.Parse(id), userId);
    return Ok(simulation);
  }

  [HttpPatch]
  [Route("{id}/cancel")]
  public async Task<IActionResult> CancelSimulation(
    [FromRoute] string id,
    [FromHeader(Name = "x-user-id")][Required] string userId
  )
  {
    var simulationId = await _simulationService.Cancel(Guid.Parse(id), userId);
    return Accepted($"/simulations/{simulationId}");
  }

  [HttpPatch]
  [Route("{id}/propose")]
  public async Task<IActionResult> ProposeSimulation(
    [FromRoute] string id,
    [FromHeader(Name = "x-user-id")][Required] string userId
  )
  {
    var simulationId = await _simulationService.Propose(Guid.Parse(id), userId);
    return Accepted($"/simulations/{simulationId}");
  }

  [HttpPatch]
  [Route("{id}/accept")]
  public async Task<IActionResult> AcceptSimulation(
    [FromRoute] string id,
    [FromHeader(Name = "x-user-id")][Required] string userId
  )
  {
    var simulationId = await _simulationService.Finish(Guid.Parse(id), userId);
    return Accepted($"/simulations/{simulationId}");
  }

}