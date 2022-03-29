namespace src.domain.common.interfaces;

public interface IDatabase<T>
{
  Task<string> Persist(string PK, string SK, T data);
  Task<T> Find(string PK, string SK);
}
