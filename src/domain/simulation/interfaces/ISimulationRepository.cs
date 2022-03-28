using src.domain.simulation.entities;

namespace src.domain.simulation.interfaces;

public interface ISimulationRepository
{
  String Persist(Simulation simulation);
  Simulation Find(Guid id, string UserId);
}