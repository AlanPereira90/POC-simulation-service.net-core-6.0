namespace simulation_service.src.domain.simulation.entities;

public class Plan
{
  public int Installment { get; set; }
  public Percentages percentages { get; set; }
  public Amounts amounts { get; set; }
}
