namespace simulation_service.src.domain.simulation.entities;

public class Plan
{
  public Plan(int installment, Percentages percentages, Amounts amounts)
  {
    this.Installment = installment;
    this.percentages = percentages;
    this.amounts = amounts;

  }
  public int Installment { get; private set; }
  public Percentages percentages { get; private set; }
  public Amounts amounts { get; private set; }
}
