using Amazon.DynamoDBv2;

namespace src.domain.infra;

public class DyanamoDB
{
  private AmazonDynamoDBClient _connection;
  public DyanamoDB(IConfiguration configuration)
  {
    AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
    clientConfig.ServiceURL = configuration["DynamoDB:Endpoint"];
    clientConfig.AuthenticationRegion = configuration["DynamoDB:Region"];

    _connection = new AmazonDynamoDBClient(clientConfig);
  }

  public AmazonDynamoDBClient Connection => _connection;
}
