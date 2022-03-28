namespace src.domain.simulation.types;

public class Amounts
{
  public Amounts(double bankSlip, double iof, double installment, double insurance, double creditOpeningFee, double hiring, double owed)
  {
    this.BankSlip = bankSlip;
    this.Iof = iof;
    this.Installment = installment;
    this.Insurance = insurance;
    this.CreditOpeningFee = creditOpeningFee;
    this.Hiring = hiring;
    this.Owed = owed;

  }
  public double BankSlip { get; set; }
  public double Iof { get; set; }
  public double Installment { get; set; }
  public double Insurance { get; set; }
  public double CreditOpeningFee { get; set; }
  public double Hiring { get; set; }
  public double Owed { get; set; }
}
