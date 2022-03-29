namespace src.domain.common.interfaces;

public interface IDatabase
{
  Task<string> Persist(string PK, string SK, Dictionary<string, object> data);
  Task<Dictionary<string, object>> Find(string PK, string SK);
}
