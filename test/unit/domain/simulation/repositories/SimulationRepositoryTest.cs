using Xunit;
using Moq;
using System;
using System.Threading.Tasks;

using src.domain.simulation.entities;
using src.domain.simulation.repositories;
using src.domain.common.interfaces;

using test.unit.domain.simulation.helpers;


namespace test.unit.domain.simulation.repositories;

public class SimulationRepositoryTest
{
  private Mock<IDatabase<Simulation>> _mockDatabase;

  public SimulationRepositoryTest()
  {
    _mockDatabase = new Mock<IDatabase<Simulation>>();
  }

  [Fact(DisplayName = "should persist and return a valid id")]
  public async void Persist()
  {
    Simulation simulation = SimulationBuilder.build();
    var expectedResult = Guid.NewGuid().ToString();

    _mockDatabase.Setup(r =>
      r.Persist(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Simulation>())
    ).Returns(Task.FromResult(expectedResult));

    SimulationRepository repository = new SimulationRepository(_mockDatabase.Object);

    var result = await repository.Persist(simulation);

    Assert.Equal(expectedResult, result);
    _mockDatabase.Verify(r =>
      r.Persist(simulation.Id.ToString(), simulation.UserId, simulation), Times.Once);
  }

  [Fact(DisplayName = "should call find method from database class")]
  public async void Find()
  {
    Simulation simulation = SimulationBuilder.build();

    _mockDatabase.Setup(r =>
      r.Find(It.IsAny<string>(), It.IsAny<string>())
    ).Returns(Task.FromResult(simulation));

    SimulationRepository repository = new SimulationRepository(_mockDatabase.Object);

    var result = await repository.Find(simulation.Id, simulation.UserId);

    Assert.Equal(simulation, result);
    _mockDatabase.Verify(r =>
      r.Find(simulation.Id.ToString(), simulation.UserId), Times.Once);
  }
}
