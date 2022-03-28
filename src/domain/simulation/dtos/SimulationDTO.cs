using src.domain.simulation.entities;
using src.domain.simulation.enums;
using src.domain.simulation.types;

namespace src.domain.simulation.dtos;

public class SimulationDTO
{
  public string Id { get; private set; }
  public string UserId { get; set; }
  public double Amount { get; set; }
  public SimulationStatus Status { get; private set; }
  public Plan Plan { get; set; }

  public Simulation ToDomain()
  {
    return new Simulation(
        userId: UserId,
        amount: Amount,
        plan: Plan
    );
  }

  public static SimulationDTO FromDomain(Simulation simulation)
  {
    return new SimulationDTO
    {
      Id = simulation.Id.ToString(),
      UserId = simulation.UserId,
      Amount = simulation.Amount,
      Status = simulation.Status,
      Plan = simulation.Plan
    };
  }
}
