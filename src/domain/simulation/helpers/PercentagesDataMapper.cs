using src.domain.simulation.types;

namespace src.domain.simulation.helpers;

public class PercentagesDataMapper
{
  public static Dictionary<string, object> ToData(Percentages percentages)
  {
    var percentagesData = new Dictionary<string, object>();
    percentagesData.Add("total_effective_costs", CostsDataMapper.ToData(percentages.TotalEffectiveCosts));
    percentagesData.Add("interests", CostsDataMapper.ToData(percentages.Interests));
    percentagesData.Add("tax_rate", percentages.TaxRate);
    return percentagesData;
  }

  public static Percentages FromData(Dictionary<string, object> data)
  {
    Percentages percentages = Activator.CreateInstance<Percentages>();

    percentages.GetType().GetProperty("TotalEffectiveCosts").SetValue(percentages, CostsDataMapper.FromData((Dictionary<string, object>)data["total_effective_costs"]));
    percentages.GetType().GetProperty("Interests").SetValue(percentages, CostsDataMapper.FromData((Dictionary<string, object>)data["interests"]));
    percentages.GetType().GetProperty("TaxRate").SetValue(percentages, data["tax_rate"]);

    return percentages;
  }
}
