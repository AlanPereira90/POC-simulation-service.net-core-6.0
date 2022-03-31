using System.Net;

using src.domain.simulation.interfaces;
using src.domain.simulation.dtos;
using src.domain.simulation.entities;

namespace src.domain.simulation.services;

public class SimulationService : ISimulationService
{
  private readonly ISimulationRepository _repository;

  public SimulationService(ISimulationRepository repository)
  {
    _repository = repository;
  }

  private async Task<Simulation> GetSimulation(Guid id, string userId)
  {
    var simulation = await _repository.Find(id, userId);
    if (simulation == null)
    {
      throw new HttpResponseException(
        message: "Simulation not found",
        statusCode: HttpStatusCode.NotFound
      );
    }
    return simulation;
  }

  public Task<string> Create(SimulationDTO simulation)
  {
    return _repository.Persist(simulation.ToDomain());
  }

  public async Task<string> Cancel(Guid Id, string UserId)
  {
    Simulation simulation = await this.GetSimulation(Id, UserId);

    if (!simulation.IsCreated())
    {
      throw new HttpResponseException(HttpStatusCode.Conflict, $"Invalid simulation status: {simulation.Status}");
    }

    simulation.Cancel("CANCELLED_BY_USER");
    return await _repository.Persist(simulation);
  }

  public async Task<string> Propose(Guid Id, string UserId)
  {
    Simulation simulation = await this.GetSimulation(Id, UserId);

    if (!simulation.IsCreated())
    {
      throw new HttpResponseException(HttpStatusCode.Conflict, $"Invalid simulation status: {simulation.Status}");
    }

    simulation.Propose();
    return await _repository.Persist(simulation);
  }

  public async Task<string> Finish(Guid Id, string UserId)
  {
    Simulation simulation = await this.GetSimulation(Id, UserId);

    if (!simulation.IsProposed())
    {
      throw new HttpResponseException(HttpStatusCode.Conflict, $"Invalid simulation status: {simulation.Status}");
    }

    simulation.Finish();
    return await _repository.Persist(simulation);
  }

  public async Task<SimulationDTO> Retrieve(Guid Id, string UserId)
  {
    return SimulationDTO.FromDomain(await this.GetSimulation(Id, UserId));
  }
}
