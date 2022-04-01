using Xunit;
using Moq;

using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

using test.builders;

namespace test.integration;

public class CreateSimulationIntegrationTest
{
  private TestClient _testClient;
  public CreateSimulationIntegrationTest()
  {
    _testClient = new TestClient();
  }

  [Fact(DisplayName = "should return 202 ACCEPTED when simulation is created")]
  public async void CreateSimulationSuccess()
  {
    var request = CreateSimulationRequestBuilder.Build();
    var simulationId = Guid.NewGuid().ToString();
    var expectedLocation = $"/simulations/{simulationId}";

    _testClient.MockDatabase.Setup(x => x.Persist(
      It.IsAny<string>(),
      It.IsAny<string>(),
      It.IsAny<Dictionary<string, object>>()
    )).Returns(Task.FromResult(simulationId));

    var response = await _testClient.Post(route: "/simulations", payload: request);

    Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
    Assert.Equal(expectedLocation, response.Headers.Location!.ToString());
  }
}
