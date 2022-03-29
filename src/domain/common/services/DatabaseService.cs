using Amazon.DynamoDBv2;

using src.domain.common.interfaces;
using src.domain.infra;
using Amazon.DynamoDBv2.Model;

namespace src.domain.infrastructure.database;

public class DatabaseService<T> : IDatabase<T>
{
  private readonly IConfiguration _configuration;
  private readonly AmazonDynamoDBClient _connection;
  public DatabaseService(IConfiguration config, DyanamoDB dyanamoDB)
  {
    _configuration = config;
    _connection = dyanamoDB.Connection;
  }
  public async Task<string> Persist(string PK, string SK, T data)
  {
    var teste = await _connection.PutItemAsync(
      tableName: _configuration["DynamoDB:TableName"],
      new Dictionary<string, AttributeValue>()
    );
    return PK;
  }

  public async Task<T> Find(string PK, string SK)
  {
    var result = await _connection.GetItemAsync(
      tableName: _configuration["DynamoDB:TableName"],
      key: new Dictionary<string, AttributeValue>()
      {
        { "PK", new AttributeValue { S = PK } },
        { "SK", new AttributeValue { S = SK } }
      }
    );
    return default(T);
  }
}
