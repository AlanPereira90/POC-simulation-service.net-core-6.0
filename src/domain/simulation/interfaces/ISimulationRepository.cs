using simulation_service.src.domain.simulation.entities;

namespace simulation_service.src.domain.simulation.interfaces;

public interface ISimulationRepository
{
  String Persist(Simulation simulation);
  Simulation Find(Guid id, string UserId);
}