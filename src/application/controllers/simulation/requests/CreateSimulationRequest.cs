using src.domain.simulation.types;

namespace src.application.controllers.simulation.requests;

public class CreateSimulationRequest
{
  public double Amount { get; set; }
  public PlanRequest Plan { get; set; }
}

public class PlanRequest
{
  public int Installment { get; set; }
  public PercentagesRequest Percentages { get; set; }
  public AmountsRequest Amounts { get; set; }
}

public class CostsRequest
{
  public double Monthly { get; set; }
  public double Annual { get; set; }
}

public class PercentagesRequest
{
  public CostsRequest TotalEffectiveCosts { get; set; }
  public CostsRequest Interests { get; set; }
  public double TaxRate { get; set; }
}

public class AmountsRequest
{
  public double BankSlip { get; set; }
  public double Iof { get; set; }
  public double Installment { get; set; }
  public double Insurance { get; set; }
  public double CreditOpeningFee { get; set; }
  public double Hiring { get; set; }
  public double Owed { get; set; }
}
