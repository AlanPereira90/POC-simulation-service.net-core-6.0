using Amazon.DynamoDBv2.Model;

namespace src.domain.infra;

public static class DynamoDBMapper
{
  private static AttributeValue BuildAttributeValue(KeyValuePair<string, object> item)
  {
    Type type = item.Value.GetType();
    AttributeValue value;

    switch (type.Name)
    {
      case "String":
        value = new AttributeValue { S = item.Value.ToString() };
        break;
      case "Int32":
      case "Double":
        value = new AttributeValue { N = item.Value.ToString() };
        break;
      case "Boolean":
        value = new AttributeValue { BOOL = (bool)item.Value };
        break;
      case "Dictionary`2":
        value = new AttributeValue { M = Marshall((Dictionary<string, object>)item.Value) };
        break;
      default:
        throw new Exception($"Type {type.Name} not supported");
    }

    return value;
  }

  private static object UnbuildAttributeValue(AttributeValue value)
  {
    if (value.S != null) return value.S;
    if (value.IsBOOLSet) return value.BOOL;
    if (value.N != null) return Double.Parse(value.N);
    if (value.M.Count > 0) return Unmarshall(value.M);

    throw new Exception($"Value {value} not supported");
  }

  public static Dictionary<string, AttributeValue> Marshall(Dictionary<string, object> data)
  {
    var result = new Dictionary<string, AttributeValue>();

    foreach (var item in data)
    {
      result.Add(item.Key, BuildAttributeValue(item));
    }

    return result;
  }

  public static Dictionary<string, object> Unmarshall(Dictionary<string, AttributeValue> data)
  {
    var result = new Dictionary<string, object>();

    foreach (var item in data)
    {
      result.Add(item.Key, UnbuildAttributeValue(item.Value));
    }

    return result;
  }
}
