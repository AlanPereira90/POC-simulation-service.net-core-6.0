using src.domain.simulation.types;

namespace src.application.controllers.simulation.requests;

public class CreateSimulationRequest
{
  public double Amount { get; set; }
  public Plan Plan { get; set; }
}
