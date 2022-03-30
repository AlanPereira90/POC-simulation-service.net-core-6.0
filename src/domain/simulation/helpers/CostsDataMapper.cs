using src.domain.simulation.types;

namespace src.domain.simulation.helpers;

public class CostsDataMapper
{
  public static Dictionary<string, object> ToData(Costs costs)
  {
    var costsData = new Dictionary<string, object>();
    costsData.Add("monthly", costs.Monthly);
    costsData.Add("annual", costs.Annual);
    return costsData;
  }

  public static Costs FromData(Dictionary<string, object> data)
  {
    Costs costs = Activator.CreateInstance<Costs>();

    costs.GetType().GetProperty("Monthly").SetValue(costs, data["monthly"]);
    costs.GetType().GetProperty("Annual").SetValue(costs, data["annual"]);

    return costs;
  }
}