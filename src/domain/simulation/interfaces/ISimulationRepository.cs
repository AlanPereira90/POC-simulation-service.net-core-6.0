using src.domain.simulation.entities;

namespace src.domain.simulation.interfaces;

public interface ISimulationRepository
{
  Task<String> Persist(Simulation simulation);
  Task<Simulation> Find(Guid id, string UserId);
}