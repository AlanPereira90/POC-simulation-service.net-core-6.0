using simulation_service.src.domain.infrastructure.interfaces;

namespace simulation_service.src.domain.infrastructure.Database;

public class Database<T> : IDatabase<T>
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
