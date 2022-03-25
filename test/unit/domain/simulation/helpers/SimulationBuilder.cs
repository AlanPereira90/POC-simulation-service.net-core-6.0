using Faker;

using simulation_service.src.domain.simulation.entities;
using simulation_service.src.domain.simulation.enums;

namespace test.unit.domain.simulation.helpers;

public static class SimulationBuilder
{
  //TODO: Change to {} instead () to avoid constructors
  public static Simulation build()
  {
    return new Simulation(
        id: System.Guid.NewGuid(),
        userId: StringFaker.AlphaNumeric(10),
        status: SimulationStatus.CREATED,
        amount: NumberFaker.Number(1, 100),
        cancellationReason: "",
        plan: new Plan(
          installment: NumberFaker.Number(1, 12),
          percentages: new Percentages(
            totalEffectiveCosts: new Costs(
              monthly: NumberFaker.Number(1, 10),
              annual: NumberFaker.Number(1, 100)
            ),
            interests: new Costs(
              monthly: NumberFaker.Number(1, 10),
              annual: NumberFaker.Number(1, 100)
            ),
            taxRate: NumberFaker.Number(1, 100)
          ),
          amounts: new Amounts(
            bankSlip: NumberFaker.Number(1, 100),
            iof: NumberFaker.Number(1, 100),
            installment: NumberFaker.Number(1, 100),
            insurance: NumberFaker.Number(1, 100),
            creditOpeningFee: NumberFaker.Number(1, 100),
            hiring: NumberFaker.Number(1, 100),
            owed: NumberFaker.Number(1, 100)
          )
        ),
        createdAt: DateTimeFaker.DateTime(),
        updatedAt: DateTimeFaker.DateTime()
    );
  }
}
