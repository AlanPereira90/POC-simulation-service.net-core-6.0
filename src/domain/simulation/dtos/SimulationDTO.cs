using src.domain.simulation.entities;

namespace src.domain.simulation.dtos;

public class SimulationDTO
{
  public string UserId { get; set; }
  public double Amount { get; set; }
  public Plan plan { get; set; }

  public Simulation ToDomain()
  {
    return new Simulation(
        userId: UserId,
        amount: Amount,
        plan: plan
    );
  }
}
