using src.domain.simulation.enums;

namespace src.domain.simulation.entities;

public class Simulation
{

  public Simulation(string userId, double amount, Plan plan)
  {
    this.Id = Guid.NewGuid();
    this.UserId = userId;
    this.status = SimulationStatus.CREATED;
    this.Amount = amount;
    this.CancellationReason = "";
    this.plan = plan;
    this.CreatedAt = DateTime.Now;
    this.UpdatedAt = DateTime.Now;
  }

  public Guid Id { get; }
  public string UserId { get; private set; }
  public SimulationStatus status { get; private set; }
  public double Amount { get; private set; }
  public string CancellationReason { get; private set; }
  public Plan plan { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public DateTime UpdatedAt { get; private set; }
}
