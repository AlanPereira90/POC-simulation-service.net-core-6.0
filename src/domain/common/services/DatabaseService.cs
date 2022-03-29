using src.domain.infrastructure.interfaces;

namespace src.domain.infrastructure.database;

public class DatabaseService<T> : IDatabase<T>
{
  public string Persist(string PK, string SK, T data)
  {
    return "";
  }

  public T Find(string PK, string SK)
  {
    return default(T);
  }
}
