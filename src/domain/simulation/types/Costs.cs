namespace src.domain.simulation.types
{
  public class Costs
  {
    public Costs(double monthly, double annual)
    {
      this.Monthly = monthly;
      this.Annual = annual;
    }
    public double Monthly { get; set; }
    public double Annual { get; set; }
  }
}