using Xunit;
using Moq;
using Faker;

using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

using src.domain.simulation.helpers;
using test.builders;

namespace test.integration;

public class AcceptSimulationIntegrationTest
{
  private TestClient _testClient;
  public AcceptSimulationIntegrationTest()
  {
    _testClient = new TestClient();
  }

  [Fact(DisplayName = "should return 202 ACCEPTED when simulation is accepted")]
  public async void CancelSimulationSuccess()
  {
    var simulation = SimulationBuilder.build();
    simulation.Propose();
    var simulationData = SimulationDataMapper.ToData(simulation);

    var expectedLocation = $"/simulations/{simulation.Id}";

    _testClient.MockDatabase.Setup(x => x.Find(
      simulation.Id.ToString(),
      simulation.UserId
    )).Returns(Task.FromResult(simulationData));

    _testClient.MockDatabase.Setup(x => x.Persist(
      simulation.Id.ToString(),
      simulation.UserId,
      It.IsAny<Dictionary<string, object>>()
    )).Returns(Task.FromResult(simulation.Id.ToString()));

    var headers = new Dictionary<string, string>
    {
      { "x-user-id", simulation.UserId }
    };

    var response = await _testClient.Patch($"/simulations/{simulation.Id}/accept", headers);

    Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
    Assert.Equal(expectedLocation, response.Headers.Location!.ToString());
  }

  [Fact(DisplayName = "should return 404 NOT_FOUND when simulation not found")]
  public async void CancelSimulationNotFound()
  {
    var id = Guid.NewGuid().ToString();
    var userId = StringFaker.AlphaNumeric(10);

    var headers = new Dictionary<string, string>
    {
      { "x-user-id", userId }
    };

    var response = await _testClient.Patch($"/simulations/{id}/accept", headers);

    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
  }

  [Fact(DisplayName = "should return 409 CONFLICT when simulation with invalid status")]
  public async void CancelSimulationConflict()
  {
    var simulation = SimulationBuilder.build();
    simulation.Cancel(TextFaker.Sentence());

    var simulationData = SimulationDataMapper.ToData(simulation);

    var expectedLocation = $"/simulations/{simulation.Id}";

    _testClient.MockDatabase.Setup(x => x.Find(
      simulation.Id.ToString(),
      simulation.UserId
    )).Returns(Task.FromResult(simulationData));

    var headers = new Dictionary<string, string>
    {
      { "x-user-id", simulation.UserId }
    };

    var response = await _testClient.Patch($"/simulations/{simulation.Id}/accept", headers);

    Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
  }
}
