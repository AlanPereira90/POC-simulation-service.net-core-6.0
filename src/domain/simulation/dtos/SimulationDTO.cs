using src.domain.simulation.entities;
using src.domain.simulation.types;
using src.application.controllers.simulation.requests;

namespace src.domain.simulation.dtos;

public class SimulationDTO
{
  public string Id { get; private set; }
  public string UserId { get; set; }
  public double Amount { get; set; }
  public String Status { get; private set; }
  public String CancellationReason { get; private set; }
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
      Status = simulation.Status.ToString(),
      CancellationReason = simulation.CancellationReason,
      Plan = simulation.Plan
    };
  }

  public static SimulationDTO FromApplication(CreateSimulationRequest request, string userId)
  {
    return new SimulationDTO
    {
      UserId = userId,
      Amount = request.Amount,
      Plan = new Plan(
        installment: request.Plan.Installment,
        percentages: new Percentages(
          totalEffectiveCosts: new Costs(
            monthly: request.Plan.Percentages.TotalEffectiveCosts.Monthly,
            annual: request.Plan.Percentages.TotalEffectiveCosts.Annual
          ),
          interests: new Costs(
            monthly: request.Plan.Percentages.Interests.Monthly,
            annual: request.Plan.Percentages.Interests.Annual
          ),
          taxRate: request.Plan.Percentages.TaxRate
        ),
        amounts: new Amounts(
          bankSlip: request.Plan.Amounts.BankSlip,
          iof: request.Plan.Amounts.Iof,
          installment: request.Plan.Amounts.Installment,
          insurance: request.Plan.Amounts.Insurance,
          creditOpeningFee: request.Plan.Amounts.CreditOpeningFee,
          hiring: request.Plan.Amounts.Hiring,
          owed: request.Plan.Amounts.Owed
        )
      )
    };
  }
}
