using simulation_service.src.domain.simulation.enums;

namespace simulation_service.src.domain.simulation.entities;

public class Simulation
{

  public Simulation(Guid id, string userId, SimulationStatus status, double amount, string cancellationReason, Plan plan, DateTime createdAt, DateTime updatedAt)
  {
    this.Id = id;
    this.UserId = userId;
    this.status = status;
    this.Amount = amount;
    this.CancellationReason = cancellationReason;
    this.plan = plan;
    this.CreatedAt = createdAt;
    this.UpdatedAt = updatedAt;

  }

  public Guid Id { get; private set; }
  public string UserId { get; private set; }
  public SimulationStatus status { get; private set; }
  public double Amount { get; private set; }
  public string CancellationReason { get; private set; }
  public Plan plan { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public DateTime UpdatedAt { get; private set; }
}
