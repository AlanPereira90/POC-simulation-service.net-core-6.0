using Faker;

using src.domain.simulation.entities;
using src.domain.simulation.enums;

namespace test.unit.domain.simulation.helpers;

public static class SimulationBuilder
{
  public static Simulation build()
  {
    return new Simulation(
        userId: StringFaker.AlphaNumeric(10),
        amount: NumberFaker.Number(1, 100),
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
        )
    );
  }
}
