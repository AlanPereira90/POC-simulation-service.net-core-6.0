using Moq;
using Microsoft.AspNetCore.TestHost;
using src.domain.infra;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace test.integration;

public class SimulationIntegrationTest
{
  private Mock<DynamoDB> _mockDynamo;

  public SimulationIntegrationTest()
  {
    _mockDynamo = new Mock<DynamoDB>();

    var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
          builder.ConfigureTestServices(services =>
          {
            services.AddSingleton<DynamoDB>(_mockDynamo.Object);
          });
        });

    var client = application.CreateClient();
  }
}
