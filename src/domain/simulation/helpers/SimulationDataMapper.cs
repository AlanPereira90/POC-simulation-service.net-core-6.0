using src.domain.simulation.entities;
using src.domain.simulation.enums;

namespace src.domain.simulation.helpers;

public static class SimulationDataMapper
{
  public static Dictionary<string, object> ToData(Simulation simulation)
  {
    var planData = PlanDataMapper.ToData(simulation.Plan);
    var simulationData = new Dictionary<string, object>();
    simulationData.Add("id", simulation.Id.ToString());
    simulationData.Add("user_id", simulation.UserId);
    simulationData.Add("status", simulation.Status.ToString());
    simulationData.Add("amount", simulation.Amount);
    simulationData.Add("cancellation_reason", simulation.CancellationReason);
    simulationData.Add("created_at", simulation.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
    simulationData.Add("updated_at", simulation.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
    simulationData.Add("plan", PlanDataMapper.ToData(simulation.Plan));
    return simulationData;
  }

  public static Simulation FromData(Dictionary<string, object> data)
  {
    Simulation simulation = new Simulation();

    simulation.GetType().GetProperty("Id").SetValue(simulation, Guid.Parse(data["id"].ToString()));
    simulation.GetType().GetProperty("UserId").SetValue(simulation, data["user_id"].ToString());
    simulation.GetType().GetProperty("Status").SetValue(simulation, Enum.Parse<SimulationStatus>(data["status"].ToString()));
    simulation.GetType().GetProperty("Amount").SetValue(simulation, double.Parse(data["amount"].ToString()));
    simulation.GetType().GetProperty("CancellationReason").SetValue(simulation, data["cancellation_reason"].ToString());
    simulation.GetType().GetProperty("CreatedAt").SetValue(simulation, DateTime.Parse(data["created_at"].ToString()));
    simulation.GetType().GetProperty("UpdatedAt").SetValue(simulation, DateTime.Parse(data["updated_at"].ToString()));
    simulation.GetType().GetProperty("Plan").SetValue(simulation, PlanDataMapper.FromData((Dictionary<string, object>)data["plan"]));

    return simulation;
  }
}
