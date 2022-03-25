using Xunit;
using Moq;
using System;
using Faker;

using simulation_service.src.domain.simulation.entities;
using simulation_service.src.domain.simulation.repositories;
using simulation_service.src.domain.infrastructure.interfaces;

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
  public void Persist()
  {
    Simulation simulation = SimulationBuilder.build();
    var expectedResult = Guid.NewGuid().ToString();

    _mockDatabase.Setup(r =>
      r.Persist(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Simulation>())
    ).Returns(expectedResult);

    SimulationRepository repository = new SimulationRepository(_mockDatabase.Object);

    var result = repository.Persist(simulation);

    Assert.Equal(expectedResult, result);
    _mockDatabase.Verify(r =>
      r.Persist(simulation.Id.ToString(), simulation.UserId, simulation), Times.Once);
  }

  [Fact(DisplayName = "should call find method from database class")]
  public void Find()
  {
    Simulation simulation = SimulationBuilder.build();

    _mockDatabase.Setup(r =>
      r.Find(It.IsAny<string>(), It.IsAny<string>())
    ).Returns(simulation);

    SimulationRepository repository = new SimulationRepository(_mockDatabase.Object);

    var result = repository.Find(simulation.Id, simulation.UserId);

    Assert.Equal(simulation, result);
    _mockDatabase.Verify(r =>
      r.Find(simulation.Id.ToString(), simulation.UserId), Times.Once);
  }
}
