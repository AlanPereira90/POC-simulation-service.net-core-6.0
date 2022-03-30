using Moq;
using Xunit;
using System;
using Faker;
using System.Threading.Tasks;

using src.domain.simulation.interfaces;
using src.domain.simulation.entities;
using src.domain.simulation.services;
using src.domain.simulation.enums;
using src.domain.simulation.dtos;
using test.unit.domain.simulation.builders;

namespace test.unit.domain.simulation.services;

public class SimulationServiceTest
{
  private Mock<ISimulationRepository> _mockRepository;

  public SimulationServiceTest()
  {
    _mockRepository = new Mock<ISimulationRepository>();
  }

  [Fact(DisplayName = "should create a simulation successfully")]
  public async void CreateSimulationTest()
  {
    var dto = SimulationDTOBuilder.build();
    var simulation = dto.ToDomain();

    _mockRepository.Setup(r =>
      r.Persist(It.IsAny<Simulation>())
    ).Returns(Task.FromResult(simulation.Id.ToString()));

    SimulationService service = new SimulationService(_mockRepository.Object);

    var result = await service.Create(dto);

    Assert.Equal(simulation.Id.ToString(), result);
    _mockRepository.Verify(r =>
      r.Persist(It.IsAny<Simulation>()), Times.Once
    );
  }

  [Fact(DisplayName = "should cancel a simulation successfully")]
  public async void CancelSimulationSuccessTest()
  {
    var simulation = SimulationBuilder.build();

    _mockRepository.Setup(r =>
      r.Find(It.IsAny<Guid>(), It.IsAny<string>())
    ).Returns(Task.FromResult(simulation));

    _mockRepository.Setup(r =>
      r.Persist(It.IsAny<Simulation>())
    ).Returns(Task.FromResult(simulation.Id.ToString()));

    SimulationService service = new SimulationService(_mockRepository.Object);

    var result = await service.Cancel(simulation.Id, simulation.UserId);

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
  public async void CancelSimulationNotFoundTest()
  {
    Guid id = Guid.NewGuid();
    string userId = StringFaker.AlphaNumeric(10);

    SimulationService service = new SimulationService(_mockRepository.Object);

    var error = await Assert.ThrowsAsync<ApplicationException>(() => service.Cancel(id, userId));
    Assert.Equal("Simulation not found", error.Message);
    _mockRepository.Verify(r =>
      r.Find(id, userId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(It.IsAny<Simulation>()), Times.Never
    );
  }

  [Fact(DisplayName = "should fail when try to cancel a simulation that status is not CREATED")]
  public async void CancelSimulationInvalidStatusTest()
  {
    var simulation = SimulationBuilder.build();
    simulation.Cancel(TextFaker.Sentence());

    _mockRepository.Setup(r =>
      r.Find(It.IsAny<Guid>(), It.IsAny<string>())
    ).Returns(Task.FromResult(simulation));

    SimulationService service = new SimulationService(_mockRepository.Object);

    var error = await Assert.ThrowsAsync<ApplicationException>(() => service.Cancel(simulation.Id, simulation.UserId));
    Assert.Equal($"Invalid simulation status: {SimulationStatus.CANCELLED}", error.Message);
    _mockRepository.Verify(r =>
      r.Find(simulation.Id, simulation.UserId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(It.IsAny<Simulation>()), Times.Never
    );
  }

  [Fact(DisplayName = "should propose a simulation successfully")]
  public async void ProposeSimulationSuccessTest()
  {
    var simulation = SimulationBuilder.build();

    _mockRepository.Setup(r =>
      r.Find(It.IsAny<Guid>(), It.IsAny<string>())
    ).Returns(Task.FromResult(simulation));

    _mockRepository.Setup(r =>
      r.Persist(It.IsAny<Simulation>())
    ).Returns(Task.FromResult(simulation.Id.ToString()));

    SimulationService service = new SimulationService(_mockRepository.Object);

    var result = await service.Propose(simulation.Id, simulation.UserId);

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
  public async void ProposeSimulationNotFoundTest()
  {
    Guid id = Guid.NewGuid();
    string userId = StringFaker.AlphaNumeric(10);

    SimulationService service = new SimulationService(_mockRepository.Object);

    var error = await Assert.ThrowsAsync<ApplicationException>(() => service.Propose(id, userId));
    Assert.Equal("Simulation not found", error.Message);
    _mockRepository.Verify(r =>
      r.Find(id, userId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(It.IsAny<Simulation>()), Times.Never
    );
  }

  [Fact(DisplayName = "should fail when try to propose a simulation that status is not CREATED")]
  public async void ProposeSimulationInvalidStatusTest()
  {
    var simulation = SimulationBuilder.build();
    simulation.Cancel(TextFaker.Sentence());

    _mockRepository.Setup(r =>
      r.Find(It.IsAny<Guid>(), It.IsAny<string>())
    ).Returns(Task.FromResult(simulation));

    SimulationService service = new SimulationService(_mockRepository.Object);

    var error = await Assert.ThrowsAsync<ApplicationException>(() => service.Propose(simulation.Id, simulation.UserId));
    Assert.Equal($"Invalid simulation status: {SimulationStatus.CANCELLED}", error.Message);
    _mockRepository.Verify(r =>
      r.Find(simulation.Id, simulation.UserId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(It.IsAny<Simulation>()), Times.Never
    );
  }

  [Fact(DisplayName = "should finish a simulation successfully")]
  public async void FinishSimulationSuccessTest()
  {
    var simulation = SimulationBuilder.build();
    simulation.Propose();

    _mockRepository.Setup(r =>
      r.Find(It.IsAny<Guid>(), It.IsAny<string>())
    ).Returns(Task.FromResult(simulation));

    _mockRepository.Setup(r =>
      r.Persist(It.IsAny<Simulation>())
    ).Returns(Task.FromResult(simulation.Id.ToString()));

    SimulationService service = new SimulationService(_mockRepository.Object);

    var result = await service.Finish(simulation.Id, simulation.UserId);

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
  public async void FinishSimulationNotFoundTest()
  {
    Guid id = Guid.NewGuid();
    string userId = StringFaker.AlphaNumeric(10);

    SimulationService service = new SimulationService(_mockRepository.Object);

    var error = await Assert.ThrowsAsync<ApplicationException>(() => service.Finish(id, userId));
    Assert.Equal("Simulation not found", error.Message);
    _mockRepository.Verify(r =>
      r.Find(id, userId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(It.IsAny<Simulation>()), Times.Never
    );
  }

  [Fact(DisplayName = "should fail when try to finish a simulation that status is not PROPOSED")]
  public async void FinishSimulationInvalidStatusTest()
  {
    var simulation = SimulationBuilder.build();
    simulation.Cancel(TextFaker.Sentence());

    _mockRepository.Setup(r =>
      r.Find(It.IsAny<Guid>(), It.IsAny<string>())
    ).Returns(Task.FromResult(simulation));

    SimulationService service = new SimulationService(_mockRepository.Object);

    var error = await Assert.ThrowsAsync<ApplicationException>(() => service.Finish(simulation.Id, simulation.UserId));
    Assert.Equal($"Invalid simulation status: {SimulationStatus.CANCELLED}", error.Message);
    _mockRepository.Verify(r =>
      r.Find(simulation.Id, simulation.UserId), Times.Once
    );
    _mockRepository.Verify(r =>
      r.Persist(It.IsAny<Simulation>()), Times.Never
    );
  }

  [Fact(DisplayName = "should retrieve a simulation successfully")]
  public async void RetrieveSimulationSuccessTest()
  {
    var simulation = SimulationBuilder.build();

    _mockRepository.Setup(r =>
      r.Find(It.IsAny<Guid>(), It.IsAny<string>())
    ).Returns(Task.FromResult(simulation));

    SimulationService service = new SimulationService(_mockRepository.Object);

    var result = await service.Retrieve(simulation.Id, simulation.UserId);

    Assert.Equal(result.Id, SimulationDTO.FromDomain(simulation).Id);
    Assert.Equal(result.UserId, SimulationDTO.FromDomain(simulation).UserId);
    Assert.Equal(result.Status, SimulationDTO.FromDomain(simulation).Status);
    _mockRepository.Verify(r =>
      r.Find(simulation.Id, simulation.UserId), Times.Once
    );
  }

  [Fact(DisplayName = "should fail when try to retrieve a simulation that not found")]
  public async void RetrieveSimulationNotFoundTest()
  {
    Guid id = Guid.NewGuid();
    string userId = StringFaker.AlphaNumeric(10);

    SimulationService service = new SimulationService(_mockRepository.Object);

    var error = await Assert.ThrowsAsync<ApplicationException>(() => service.Retrieve(id, userId));
    Assert.Equal("Simulation not found", error.Message);
    _mockRepository.Verify(r =>
      r.Find(id, userId), Times.Once
    );
  }
}
