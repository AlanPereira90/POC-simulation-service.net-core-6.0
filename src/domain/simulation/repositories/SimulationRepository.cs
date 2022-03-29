using src.domain.simulation.interfaces;
using src.domain.common.interfaces;
using src.domain.simulation.entities;

namespace src.domain.simulation.repositories;

public class SimulationRepository : ISimulationRepository
{
  private readonly IDatabase _database;

  public SimulationRepository(IDatabase database)
  {
    _database = database;
  }

  public Task<string> Persist(Simulation simulation)
  {
    return _database.Persist(simulation.Id.ToString(), simulation.UserId, simulation.ToData());
  }

  public async Task<Simulation> Find(Guid id, string UserId)
  {
    return Simulation.FromData(await _database.Find(id.ToString(), UserId));
  }
}
