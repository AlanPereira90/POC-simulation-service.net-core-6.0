using simulation_service.src.domain.simulation.interfaces;
using simulation_service.src.domain.infrastructure.interfaces;
using simulation_service.src.domain.simulation.entities;

namespace simulation_service.src.domain.simulation.repositories;

public class SimulationRepository : ISimulationRepository
{
  private readonly IDatabase<Simulation> _database;

  public SimulationRepository(IDatabase<Simulation> database)
  {
    _database = database;
  }

  public string Persist(Simulation simulation)
  {
    return _database.Persist(simulation.Id.ToString(), simulation.UserId, simulation);
  }

  public Simulation Find(Guid id, string UserId)
  {
    return _database.Find(id.ToString(), UserId);
  }
}
