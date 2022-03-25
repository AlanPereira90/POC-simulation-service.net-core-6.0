namespace simulation_service.src.domain.infrastructure.interfaces;

public interface IDatabase<T>
{
  string Persist(T data);
  T Find(string PK, string SK);
}
