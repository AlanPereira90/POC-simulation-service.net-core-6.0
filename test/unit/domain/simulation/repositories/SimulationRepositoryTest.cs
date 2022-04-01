using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using src.domain.simulation.entities;
using src.domain.simulation.repositories;
using src.domain.common.interfaces;
using src.domain.simulation.helpers;

using test.builders;


namespace test.unit.domain.simulation.repositories;

public class SimulationRepositoryTest
{
  private Mock<IDatabase> _mockDatabase;

  public SimulationRepositoryTest()
  {
    _mockDatabase = new Mock<IDatabase>();
  }

  [Fact(DisplayName = "should persist and return a valid id")]
  public async void Persist()
  {
    Simulation simulation = SimulationBuilder.build();
    var expectedResult = Guid.NewGuid().ToString();

    _mockDatabase.Setup(r =>
      r.Persist(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, object>>())
    ).Returns(Task.FromResult(expectedResult));

    SimulationRepository repository = new SimulationRepository(_mockDatabase.Object);

    var result = await repository.Persist(simulation);

    Assert.Equal(expectedResult, result);
    _mockDatabase.Verify(r =>
      r.Persist(
        simulation.Id.ToString(), simulation.UserId, It.IsAny<Dictionary<string, object>>()
      ), Times.Once);
  }

  [Fact(DisplayName = "should call find method from database class")]
  public async void Find()
  {
    Simulation simulation = SimulationBuilder.build();

    _mockDatabase.Setup(r =>
      r.Find(It.IsAny<string>(), It.IsAny<string>())
    ).Returns(Task.FromResult(SimulationDataMapper.ToData(simulation)));

    SimulationRepository repository = new SimulationRepository(_mockDatabase.Object);

    var result = await repository.Find(simulation.Id, simulation.UserId);

    Assert.Equal(simulation.Id, result.Id);
    Assert.Equal(simulation.Status, result.Status);
    Assert.Equal(simulation.UserId, result.UserId);
    Assert.Equal(simulation.Amount, result.Amount);
    Assert.Equal(simulation.Plan.Installment, result.Plan.Installment);
    _mockDatabase.Verify(r =>
      r.Find(simulation.Id.ToString(), simulation.UserId), Times.Once);
  }
}
