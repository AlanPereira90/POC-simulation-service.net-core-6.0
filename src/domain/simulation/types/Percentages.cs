namespace src.domain.simulation.types;

public class Percentages
{
  public Percentages()
  {

  }

  public Percentages(Costs totalEffectiveCosts, Costs interests, double taxRate)
  {
    this.TotalEffectiveCosts = totalEffectiveCosts;
    this.Interests = interests;
    this.TaxRate = taxRate;

  }

  public Costs TotalEffectiveCosts { get; set; }
  public Costs Interests { get; set; }
  public double TaxRate { get; set; }
}
