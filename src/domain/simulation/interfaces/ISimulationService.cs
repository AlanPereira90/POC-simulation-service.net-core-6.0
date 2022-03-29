using src.domain.simulation.dtos;

namespace src.domain.simulation.interfaces;

public interface ISimulationService
{
  Task<string> Create(SimulationDTO simulation);
  Task<string> Cancel(Guid Id, string UserId);
  Task<string> Propose(Guid Id, string UserId);
  Task<string> Finish(Guid Id, string UserId);
  Task<SimulationDTO> Retrieve(Guid Id, string UserId);

}
