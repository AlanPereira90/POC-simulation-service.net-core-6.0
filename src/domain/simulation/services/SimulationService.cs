using src.domain.simulation.interfaces;
using src.domain.simulation.dtos;

namespace src.domain.simulation.services;

public class SimulationService : ISimulationService
{
  public string CreateSimulation(SimulationDTO simulation)
  {
    throw new System.NotImplementedException();
  }

  public string CancelSimulation(Guid id, string UserId)
  {
    throw new System.NotImplementedException();
  }

  public string ProposeSimulation(Guid id, string UserId)
  {
    throw new System.NotImplementedException();
  }

  public string FinishSimulation(Guid id, string UserId)
  {
    throw new System.NotImplementedException();
  }
}
