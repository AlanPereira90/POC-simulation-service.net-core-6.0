using Amazon.DynamoDBv2;

namespace src.domain.infra;

public class DynamoDB
{
  private AmazonDynamoDBClient _connection;
  public DynamoDB(IConfiguration configuration)
  {
    AmazonDynamoDBConfig clientConfig = new AmazonDynamoDBConfig();
    clientConfig.ServiceURL = configuration["DynamoDB:Endpoint"];
    clientConfig.AuthenticationRegion = configuration["DynamoDB:Region"];

    _connection = new AmazonDynamoDBClient(clientConfig);
  }

  public AmazonDynamoDBClient Connection => _connection;
}
