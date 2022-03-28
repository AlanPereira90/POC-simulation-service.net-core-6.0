using src.application.controllers.simulation.requests;
using src.domain.simulation.interfaces;
using src.domain.simulation.dtos;
using src.domain.simulation.types;

namespace src.application.controllers;

public class CreateSimulationController
{
  private readonly ISimulationService _simulationService;

  public CreateSimulationController(ISimulationService simulationService)
  {
    this._simulationService = simulationService;
  }

  public static string Route => "/simulations";
  public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
  public static Delegate Handle => Action;

  public static async Task<IResult> Action(CresteSimulationRequest request, HttpContext http)
  {
    var id = _simulationService.Create(
        new SimulationDTO
        {
          UserId = http.Request.Headers.TryGetValue("x-user-id", out var userId) ? userId : "",
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
        }
    );

    return Results.Ok(new { id });
  }
}
