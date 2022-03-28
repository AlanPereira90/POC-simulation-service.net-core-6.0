using src.domain.simulation.entities;
using src.domain.simulation.enums;
using src.domain.simulation.types;
using src.application.controllers.simulation.requests;

namespace src.domain.simulation.dtos;

public class SimulationDTO
{
  public string Id { get; private set; }
  public string UserId { get; set; }
  public double Amount { get; set; }
  public SimulationStatus Status { get; private set; }
  public Plan Plan { get; set; }

  public Simulation ToDomain()
  {
    return new Simulation(
        userId: UserId,
        amount: Amount,
        plan: Plan
    );
  }

  public static SimulationDTO FromDomain(Simulation simulation)
  {
    return new SimulationDTO
    {
      Id = simulation.Id.ToString(),
      UserId = simulation.UserId,
      Amount = simulation.Amount,
      Status = simulation.Status,
      Plan = simulation.Plan
    };
  }

  public static SimulationDTO FromRequest(CresteSimulationRequest request, string UserId)
  {
    return new SimulationDTO
    {
      UserId = UserId,
      Amount = request.Amount,
      Plan = new Plan(
        request.Plan.Installments,
        new Percentages(
            new Costs(
              request.Plan.Rate.TotalEffectiveCost.Monthly,
              request.Plan.Rate.TotalEffectiveCost.Annual
            ),
            new Costs(
              request.Plan.Rate.Interest.Monthly,
              request.Plan.Rate.Interest.Annual
            ),
            request.Plan.Rate.Iof
        ),
      new Amounts(
          request.Plan.Value.BankSlip,
          request.Plan.Value.Iof,
          request.Plan.Value.Installment,
          request.Plan.Value.Insurance,
          request.Plan.Value.CreditOpeningFee,
          request.Plan.Value.Hiring,
          request.Plan.Value.Owed
        )
      )

    };
  }
}
