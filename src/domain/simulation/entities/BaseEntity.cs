namespace simulation_service.src.domain.simulation.entities;

public class BaseEntity
{
  public Guid Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

}
