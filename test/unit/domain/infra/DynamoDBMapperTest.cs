using Xunit;
using System;
using System.Collections.Generic;
using KellermanSoftware.CompareNetObjects;

using Amazon.DynamoDBv2.Model;

using src.domain.infra;
using src.domain.simulation.enums;


namespace test.unit.domain.infra;

public class DynamoDBMapperTest
{
  private CompareLogic compareLogic;
  private Dictionary<string, object> randomObject;
  private Dictionary<string, AttributeValue> marshalledObject;

  public DynamoDBMapperTest()
  {
    compareLogic = new CompareLogic();

    randomObject = new Dictionary<string, object>
    {
      { "key1", "value1" },
      { "key2", 2 },
      { "key3", true },
      { "key4", new Dictionary<string, object>
        {
          { "key4-1", "value4-1" },
          { "key4-2", 2 },
          { "key4-3", true }
        }
      }
    };

    marshalledObject = new Dictionary<string, AttributeValue>
    {
      { "key1", new AttributeValue { S = "value1" } },
      { "key2", new AttributeValue { N = "2" } },
      { "key3", new AttributeValue { BOOL = true } },
      { "key4", new AttributeValue { M = new Dictionary<string, AttributeValue>
        {
          { "key4-1", new AttributeValue { S = "value4-1" } },
          { "key4-2", new AttributeValue { N = "2" } },
          { "key4-3", new AttributeValue { BOOL = true } }
        }
      }}
    };
  }

  [Fact(DisplayName = "Should marshall a random object successfully")]
  public void MarsahllSuccessTest()
  {
    var result = DynamoDBMapper.Marshall(randomObject);

    Assert.True(compareLogic.Compare(result, marshalledObject).AreEqual);
  }

  [Fact(DisplayName = "Should fail with an unexpected type on object")]
  public void MarshallFailTest()
  {
    var randomObject = new Dictionary<string, object>
    {
        { "key1", "value1" },
        { "key2", 2 },
        { "key3",  SimulationStatus.CANCELLED }
    };

    var error = Assert.Throws<Exception>(() => DynamoDBMapper.Marshall(randomObject));
    Assert.Equal("Type SimulationStatus not supported", error.Message);
  }

  [Fact(DisplayName = "Should unmarshall a marshalled object successfully")]
  public void UnmarsahllSuccessTest()
  {
    var result = DynamoDBMapper.Unmarshall(marshalledObject);

    Assert.True(compareLogic.Compare(result, randomObject).AreEqual);
  }

  [Fact(DisplayName = "Should fail with an unexpected type on marshalled object")]
  public void UnmarshallFailTest()
  {
    var marshalledObject = new Dictionary<string, AttributeValue>
    {
        { "key1", new AttributeValue { S = "value1" } },
        { "key2", new AttributeValue { N = "2" } },
        { "key3", new AttributeValue { SS = new List<string>() } }
    };

    Assert.Throws<Exception>(() => DynamoDBMapper.Unmarshall(marshalledObject));
  }
}
