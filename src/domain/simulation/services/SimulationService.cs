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

  private Simulation GetSimulation(Guid id, string userId)
  {
    var simulation = _repository.Find(id, userId);
    if (simulation == null)
    {
      throw new ApplicationException("Simulation not found");
    }
    return simulation;
  }

  public string Create(SimulationDTO simulation)
  {
    return _repository.Persist(simulation.ToDomain());
  }

  public string Cancel(Guid Id, string UserId)
  {
    Simulation simulation = this.GetSimulation(Id, UserId);

    if (!simulation.IsCreated())
    {
      throw new ApplicationException($"Invalid simulation status: {simulation.Status}");
    }

    simulation.Cancel("CANCELLED_BY_USER");
    return _repository.Persist(simulation);
  }

  public string Propose(Guid Id, string UserId)
  {
    Simulation simulation = this.GetSimulation(Id, UserId);

    if (!simulation.IsCreated())
    {
      throw new ApplicationException($"Invalid simulation status: {simulation.Status}");
    }

    simulation.Propose();
    return _repository.Persist(simulation);
  }

  public string Finish(Guid Id, string UserId)
  {
    Simulation simulation = this.GetSimulation(Id, UserId);

    if (!simulation.IsProposed())
    {
      throw new ApplicationException($"Invalid simulation status: {simulation.Status}");
    }

    simulation.Finish();
    return _repository.Persist(simulation);
  }

  public SimulationDTO Retrieve(Guid Id, string UserId)
  {
    return SimulationDTO.FromDomain(this.GetSimulation(Id, UserId));
  }
}
