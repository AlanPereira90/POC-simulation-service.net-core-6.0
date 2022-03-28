using Moq;
using Xunit;
using System;
using Faker;

using src.domain.simulation.interfaces;
using src.domain.simulation.entities;
using src.domain.simulation.services;
using src.domain.simulation.enums;
using src.domain.simulation.dtos;
using test.unit.domain.simulation.helpers;

namespace test.unit.domain.simulation.services;

public class SimulationServiceTest
{
  private Mock<ISimulationRepository> _mockRepository;

  public SimulationServiceTest()
  {
    _mockRepository = new Mock<ISimulationRepository>();
  }

  [Fact(DisplayName = "should create a simulation successfully")]
  public void CreateSimulationTest()
  {
    var dto = SimulationDTOBuilder.build();
    var simulation = dto.ToDomain();

    _mockRepository.Setup(r =>
      r.Persist(It.IsAny<Simulation>())
    ).Returns(simulation.Id.ToString());

    SimulationService service = new SimulationService(_mockRepository.Object);

    var result = service.Create(dto);

    Assert.Equal(simulation.Id.ToString(), result);
    _mockRepository.Verify(r =>
      r.Persist(It.IsAny<Simulation>()), Times.Once
    );
  }

  [Fact(DisplayName = "should cancel a simulation successfully")]
  public void CancelSimulationSuccessTest()
  {
    var simulation = SimulationBuilder.build();

    _mockRepository.Setup(r =>
      r.Find(It.IsAny<Guid>(), It.IsAny<string>())
    ).Returns(simulation);

    _mockRepository.Setup(r =>
      r.Persist(It.IsAny<Simulation>())
    ).Returns(simulation.Id.ToString());

    SimulationService service = new SimulationService(_mockRepository.Object);

    var result = service.Cancel(simulation.Id, simulation.UserId);

    Assert.Equal(simulation.Id.ToString(), result);
    Assert.Equal(simulation.Status, SimulationStatus.CANCELLED);
    _mockRepository.Verify(r =>
      r.Find(simulation.Id, simulation.UserId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(simulation), Times.Once
    );
  }

  [Fact(DisplayName = "should fail when try to cancel a simulation that not found")]
  public void CancelSimulationNotFoundTest()
  {
    Guid id = Guid.NewGuid();
    string userId = StringFaker.AlphaNumeric(10);

    SimulationService service = new SimulationService(_mockRepository.Object);

    var error = Assert.Throws<ApplicationException>(() => service.Cancel(id, userId));
    Assert.Equal("Simulation not found", error.Message);
    _mockRepository.Verify(r =>
      r.Find(id, userId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(It.IsAny<Simulation>()), Times.Never
    );
  }

  [Fact(DisplayName = "should fail when try to cancel a simulation that status is not CREATED")]
  public void CancelSimulationInvalidStatusTest()
  {
    var simulation = SimulationBuilder.build();
    simulation.Cancel(TextFaker.Sentence());

    _mockRepository.Setup(r =>
      r.Find(It.IsAny<Guid>(), It.IsAny<string>())
    ).Returns(simulation);

    SimulationService service = new SimulationService(_mockRepository.Object);

    var error = Assert.Throws<ApplicationException>(() => service.Cancel(simulation.Id, simulation.UserId));
    Assert.Equal($"Invalid simulation status: {SimulationStatus.CANCELLED}", error.Message);
    _mockRepository.Verify(r =>
      r.Find(simulation.Id, simulation.UserId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(It.IsAny<Simulation>()), Times.Never
    );
  }

  [Fact(DisplayName = "should propose a simulation successfully")]
  public void ProposeSimulationSuccessTest()
  {
    var simulation = SimulationBuilder.build();

    _mockRepository.Setup(r =>
      r.Find(It.IsAny<Guid>(), It.IsAny<string>())
    ).Returns(simulation);

    _mockRepository.Setup(r =>
      r.Persist(It.IsAny<Simulation>())
    ).Returns(simulation.Id.ToString());

    SimulationService service = new SimulationService(_mockRepository.Object);

    var result = service.Propose(simulation.Id, simulation.UserId);

    Assert.Equal(simulation.Id.ToString(), result);
    Assert.Equal(simulation.Status, SimulationStatus.PROPOSED);
    _mockRepository.Verify(r =>
      r.Find(simulation.Id, simulation.UserId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(simulation), Times.Once
    );
  }

  [Fact(DisplayName = "should fail when try to propose a simulation that not found")]
  public void ProposeSimulationNotFoundTest()
  {
    Guid id = Guid.NewGuid();
    string userId = StringFaker.AlphaNumeric(10);

    SimulationService service = new SimulationService(_mockRepository.Object);

    var error = Assert.Throws<ApplicationException>(() => service.Propose(id, userId));
    Assert.Equal("Simulation not found", error.Message);
    _mockRepository.Verify(r =>
      r.Find(id, userId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(It.IsAny<Simulation>()), Times.Never
    );
  }

  [Fact(DisplayName = "should fail when try to propose a simulation that status is not CREATED")]
  public void ProposeSimulationInvalidStatusTest()
  {
    var simulation = SimulationBuilder.build();
    simulation.Cancel(TextFaker.Sentence());

    _mockRepository.Setup(r =>
      r.Find(It.IsAny<Guid>(), It.IsAny<string>())
    ).Returns(simulation);

    SimulationService service = new SimulationService(_mockRepository.Object);

    var error = Assert.Throws<ApplicationException>(() => service.Propose(simulation.Id, simulation.UserId));
    Assert.Equal($"Invalid simulation status: {SimulationStatus.CANCELLED}", error.Message);
    _mockRepository.Verify(r =>
      r.Find(simulation.Id, simulation.UserId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(It.IsAny<Simulation>()), Times.Never
    );
  }

  [Fact(DisplayName = "should finish a simulation successfully")]
  public void FinishSimulationSuccessTest()
  {
    var simulation = SimulationBuilder.build();
    simulation.Propose();

    _mockRepository.Setup(r =>
      r.Find(It.IsAny<Guid>(), It.IsAny<string>())
    ).Returns(simulation);

    _mockRepository.Setup(r =>
      r.Persist(It.IsAny<Simulation>())
    ).Returns(simulation.Id.ToString());

    SimulationService service = new SimulationService(_mockRepository.Object);

    var result = service.Finish(simulation.Id, simulation.UserId);

    Assert.Equal(simulation.Id.ToString(), result);
    Assert.Equal(simulation.Status, SimulationStatus.READY);
    _mockRepository.Verify(r =>
      r.Find(simulation.Id, simulation.UserId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(simulation), Times.Once
    );
  }

  [Fact(DisplayName = "should fail when try to finish a simulation that not found")]
  public void FinishSimulationNotFoundTest()
  {
    Guid id = Guid.NewGuid();
    string userId = StringFaker.AlphaNumeric(10);

    SimulationService service = new SimulationService(_mockRepository.Object);

    var error = Assert.Throws<ApplicationException>(() => service.Finish(id, userId));
    Assert.Equal("Simulation not found", error.Message);
    _mockRepository.Verify(r =>
      r.Find(id, userId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(It.IsAny<Simulation>()), Times.Never
    );
  }

  [Fact(DisplayName = "should fail when try to finish a simulation that status is not PROPOSED")]
  public void FinishSimulationInvalidStatusTest()
  {
    var simulation = SimulationBuilder.build();
    simulation.Cancel(TextFaker.Sentence());

    _mockRepository.Setup(r =>
      r.Find(It.IsAny<Guid>(), It.IsAny<string>())
    ).Returns(simulation);

    SimulationService service = new SimulationService(_mockRepository.Object);

    var error = Assert.Throws<ApplicationException>(() => service.Finish(simulation.Id, simulation.UserId));
    Assert.Equal($"Invalid simulation status: {SimulationStatus.CANCELLED}", error.Message);
    _mockRepository.Verify(r =>
      r.Find(simulation.Id, simulation.UserId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(It.IsAny<Simulation>()), Times.Never
    );
  }

  [Fact(DisplayName = "should retrieve a simulation successfully")]
  public void RetrieveSimulationSuccessTest()
  {
    var simulation = SimulationBuilder.build();

    _mockRepository.Setup(r =>
      r.Find(It.IsAny<Guid>(), It.IsAny<string>())
    ).Returns(simulation);

    SimulationService service = new SimulationService(_mockRepository.Object);

    var result = service.Retrieve(simulation.Id, simulation.UserId);

    Assert.Equal(result.Id, SimulationDTO.FromDomain(simulation).Id);
    Assert.Equal(result.UserId, SimulationDTO.FromDomain(simulation).UserId);
    Assert.Equal(result.Status, SimulationDTO.FromDomain(simulation).Status);
    _mockRepository.Verify(r =>
      r.Find(simulation.Id, simulation.UserId), Times.Once
    );
  }

  [Fact(DisplayName = "should fail when try to retrieve a simulation that not found")]
  public void RetrieveSimulationNotFoundTest()
  {
    Guid id = Guid.NewGuid();
    string userId = StringFaker.AlphaNumeric(10);

    SimulationService service = new SimulationService(_mockRepository.Object);

    var error = Assert.Throws<ApplicationException>(() => service.Retrieve(id, userId));
    Assert.Equal("Simulation not found", error.Message);
    _mockRepository.Verify(r =>
      r.Find(id, userId), Times.Once
    );
  }
}
