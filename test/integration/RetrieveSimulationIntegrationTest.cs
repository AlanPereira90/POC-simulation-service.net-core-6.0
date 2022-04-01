using Xunit;
using Moq;
using Faker;
using KellermanSoftware.CompareNetObjects;
using Newtonsoft.Json;

using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

using test.builders;
using src.domain.simulation.helpers;
using src.domain.simulation.dtos;

namespace test.integration;

public class RetrieveSimulationIntegrationTest
{
  private TestClient _testClient;
  private CompareLogic _compareLogic;
  public RetrieveSimulationIntegrationTest()
  {
    _testClient = new TestClient();
    _compareLogic = new CompareLogic(
        new ComparisonConfig()
        {
          IgnoreObjectTypes = true
        }
    );
  }

  [Fact(DisplayName = "should return 200 OK when simulation is retrieved")]
  public async void RetrieveSimulationSuccess()
  {
    var simulation = SimulationBuilder.build();
    var simulationData = SimulationDataMapper.ToData(simulation);
    var simulationDto = SimulationDTO.FromDomain(simulation);

    _testClient.MockDatabase.Setup(x => x.Find(
        simulation.Id.ToString(),
        simulation.UserId
    )).Returns(Task.FromResult(simulationData));

    var headers = new Dictionary<string, string>
        {
            { "x-user-id", simulation.UserId }
        };

    var response = await _testClient.Get($"/simulations/{simulation.Id}", headers);
    var responseBody = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.True(_compareLogic.Compare(simulationDto, responseBody).AreEqual);
  }

  [Fact(DisplayName = "should return 404 Not Found when simulation is not found")]
  public async void RetrieveSimulationNotFound()
  {
    var simulationId = Guid.NewGuid().ToString();
    var userId = StringFaker.AlphaNumeric(10);

    var headers = new Dictionary<string, string>
        {
            { "x-user-id", userId }
        };

    var response = await _testClient.Get($"/simulations/{simulationId}", headers);
    var responseBody = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
  }
}
