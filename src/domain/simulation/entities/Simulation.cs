using simulation_service.src.domain.simulation.enums;

namespace simulation_service.src.domain.simulation.entities;

public class Simulation : BaseEntity
{
  public string UserId { get; set; }
  public SimulationStatus status { get; set; }
  public double Amount { get; set; }

  public string CancellationReason { get; set; }
  public Plan plan { get; set; }
}
