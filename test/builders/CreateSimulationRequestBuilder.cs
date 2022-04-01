using Faker;

using src.application.controllers.simulation.requests;

namespace test.builders;

public static class CreateSimulationRequestBuilder
{
  public static CreateSimulationRequest Build()
  {
    return new CreateSimulationRequest
    {
      Amount = NumberFaker.Number(1, 100000),
      Plan = new PlanRequest
      {
        Installment = NumberFaker.Number(1, 100),
        Percentages = new PercentagesRequest
        {
          TotalEffectiveCosts = new CostsRequest
          {
            Monthly = NumberFaker.Number(1, 100),
            Annual = NumberFaker.Number(1, 100)
          },
          Interests = new CostsRequest
          {
            Monthly = NumberFaker.Number(1, 100),
            Annual = NumberFaker.Number(1, 100)
          },
          TaxRate = NumberFaker.Number(1, 100)
        },
        Amounts = new AmountsRequest
        {
          BankSlip = NumberFaker.Number(1, 100),
          Iof = NumberFaker.Number(1, 100),
          Installment = NumberFaker.Number(1, 100),
          Insurance = NumberFaker.Number(1, 100),
          CreditOpeningFee = NumberFaker.Number(1, 100),
          Hiring = NumberFaker.Number(1, 100),
          Owed = NumberFaker.Number(1, 100)
        }
      }
    };
  }
}
