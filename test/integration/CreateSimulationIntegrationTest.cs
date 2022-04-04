using Xunit;
using Moq;
using Faker;

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
    var userId = StringFaker.AlphaNumeric(10);
    var expectedLocation = $"/simulations/{simulationId}";

    _testClient.MockDatabase.Setup(x => x.Persist(
      It.IsAny<string>(),
      It.IsAny<string>(),
      It.IsAny<Dictionary<string, object>>()
    )).Returns(Task.FromResult(simulationId));

    var headers = new Dictionary<string, string>
    {
        { "x-user-id", userId }
    };

    var response = await _testClient.Post("/simulations", headers, request);

    Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
    Assert.Equal(expectedLocation, response.Headers.Location!.ToString());
  }

  [Fact(DisplayName = "should return 400 BAD REQUEST with an invalid payload")]
  public async void CreateSimulationInvalidPayload()
  {
    var request = new
    {
      foo = "bar"
    };
    var userId = StringFaker.AlphaNumeric(10);

    var headers = new Dictionary<string, string>
    {
        { "x-user-id", userId }
    };

    var response = await _testClient.Post("/simulations", headers, request);

    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
  }

  [Fact(DisplayName = "should return 400 BAD REQUEST without an user id on headers")]
  public async void CreateSimulationInvalidHeaders()
  {
    var request = CreateSimulationRequestBuilder.Build();

    var response = await _testClient.Post("/simulations", payload: request);

    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
  }
}
