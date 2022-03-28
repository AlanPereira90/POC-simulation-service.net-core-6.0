namespace src.domain.infrastructure.interfaces;

public interface IDatabase<T>
{
  string Persist(string PK, string SK, T data);
  T Find(string PK, string SK);
}
