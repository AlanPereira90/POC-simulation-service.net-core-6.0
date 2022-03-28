using src.domain.simulation.enums;

namespace src.domain.simulation.entities;

public class Simulation
{

  public Simulation(string userId, double amount, Plan plan)
  {
    this.Id = Guid.NewGuid();
    this.UserId = userId;
    this.Status = SimulationStatus.CREATED;
    this.Amount = amount;
    this.CancellationReason = "";
    this.Plan = plan;
    this.CreatedAt = DateTime.Now;
    this.UpdatedAt = DateTime.Now;
  }

  public void Cancel(string reason)
  {
    this.Status = SimulationStatus.CANCELLED;
    this.CancellationReason = reason;
    this.Update();
  }

  public void Propose()
  {
    this.Status = SimulationStatus.PROPOSED;
    this.Update();
  }

  public void Finish()
  {
    this.Status = SimulationStatus.READY;
    this.Update();
  }

  public bool IsCreated()
  {
    return this.Status == SimulationStatus.CREATED;
  }

  public bool IsProposed()
  {
    return this.Status == SimulationStatus.PROPOSED;
  }

  private void Update()
  {
    this.UpdatedAt = DateTime.Now;
  }

  public Guid Id { get; }
  public string UserId { get; private set; }
  public SimulationStatus Status { get; private set; }
  public double Amount { get; private set; }
  public string CancellationReason { get; private set; }
  public Plan Plan { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public DateTime UpdatedAt { get; private set; }
}
