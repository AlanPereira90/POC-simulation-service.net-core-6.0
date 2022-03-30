namespace src.domain.simulation.types;

public class Plan
{
  public Plan()
  {

  }

  public Plan(int installment, Percentages percentages, Amounts amounts)
  {
    this.Installment = installment;
    this.Percentages = percentages;
    this.Amounts = amounts;

  }
  public int Installment { get; private set; }
  public Percentages Percentages { get; private set; }
  public Amounts Amounts { get; private set; }
}
