using Xunit;

using test.builders;
using src.domain.simulation.helpers;

namespace test.unit.domain.simulation.helpers;

public class SimulationDataMapperTest
{
  [Fact(DisplayName = "shoud convert a simulation into a data object successfuuly")]
  public void ToDataTest()
  {
    var simulation = SimulationBuilder.build();

    var result = SimulationDataMapper.ToData(simulation);

    Assert.Equal(simulation.Id.ToString(), result["id"]);
    Assert.Equal(simulation.UserId, result["user_id"]);
    Assert.Equal(simulation.Status.ToString(), result["status"]);
  }

  [Fact(DisplayName = "shoud convert a data object into a simulation successfuuly")]
  public void FromData()
  {
    var simulation = SimulationBuilder.build();
    var data = SimulationDataMapper.ToData(simulation);
    var newSimulation = SimulationDataMapper.FromData(data);

    Assert.Equal(newSimulation.Id.ToString(), simulation.Id.ToString());
    Assert.Equal(newSimulation.UserId, simulation.UserId);
    Assert.Equal(newSimulation.Status.ToString(), simulation.Status.ToString());
    Assert.Equal(
      newSimulation.Plan.Percentages.TotalEffectiveCosts.Monthly,
      simulation.Plan.Percentages.TotalEffectiveCosts.Monthly
    );
  }
}
