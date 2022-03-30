using src.domain.simulation.types;

namespace src.domain.simulation.helpers;

public class PlanDataMapper
{
  public static Dictionary<string, object> ToData(Plan plan)
  {
    var planData = new Dictionary<string, object>();
    planData.Add("installment", plan.Installment);
    planData.Add("percentages", PercentagesDataMapper.ToData(plan.Percentages));
    planData.Add("amounts", AmountsDataMapper.ToData(plan.Amounts));
    return planData;
  }

  public static Plan FromData(Dictionary<string, object> data)
  {
    Plan plan = Activator.CreateInstance<Plan>();

    plan.GetType().GetProperty("Installment").SetValue(plan, data["installment"]);
    plan.GetType().GetProperty("Percentages").SetValue(plan, PercentagesDataMapper.FromData((Dictionary<string, object>)data["percentages"]));
    plan.GetType().GetProperty("Amounts").SetValue(plan, AmountsDataMapper.FromData((Dictionary<string, object>)data["amounts"]));

    return plan;
  }
}
