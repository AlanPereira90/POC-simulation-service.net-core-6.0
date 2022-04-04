using System.ComponentModel.DataAnnotations;

namespace src.application.controllers.simulation.requests;

public class CreateSimulationRequest
{
  [Required] public double Amount { get; set; }
  [Required] public PlanRequest Plan { get; set; }
}

public class PlanRequest
{
  [Required] public int Installment { get; set; }
  [Required] public PercentagesRequest Percentages { get; set; }
  [Required] public AmountsRequest Amounts { get; set; }
}

public class CostsRequest
{
  [Required] public double Monthly { get; set; }
  [Required] public double Annual { get; set; }
}

public class PercentagesRequest
{
  [Required] public CostsRequest TotalEffectiveCosts { get; set; }
  [Required] public CostsRequest Interests { get; set; }
  [Required] public double TaxRate { get; set; }
}

public class AmountsRequest
{
  [Required] public double BankSlip { get; set; }
  [Required] public double Iof { get; set; }
  [Required] public double Installment { get; set; }
  [Required] public double Insurance { get; set; }
  [Required] public double CreditOpeningFee { get; set; }
  [Required] public double Hiring { get; set; }
  [Required] public double Owed { get; set; }
}
