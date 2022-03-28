using src.domain.simulation.dtos;

namespace src.domain.simulation.interfaces;

public interface ISimulationService
{
  string CreateSimulation(SimulationDTO simulation);
  string CancelSimulation(Guid id, string UserId);
  string ProposeSimulation(Guid id, string UserId);
  string FinishSimulation(Guid id, string UserId);

}
