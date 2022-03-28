using src.domain.simulation.dtos;

namespace src.domain.simulation.interfaces;

public interface ISimulationService
{
  string Create(SimulationDTO simulation);
  string Cancel(Guid Id, string UserId);
  string Propose(Guid Id, string UserId);
  string Finish(Guid Id, string UserId);
  SimulationDTO Retrieve(Guid Id, string UserId);

}
