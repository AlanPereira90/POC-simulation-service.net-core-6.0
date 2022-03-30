using src.domain.simulation.types;

namespace src.domain.simulation.helpers;

public class AmountsDataMapper
{
  public static Dictionary<string, object> ToData(Amounts amounts)
  {
    var amountsData = new Dictionary<string, object>();
    amountsData.Add("bankSlip", amounts.BankSlip);
    amountsData.Add("iof", amounts.Iof);
    amountsData.Add("installment", amounts.Installment);
    amountsData.Add("insurance", amounts.Insurance);
    amountsData.Add("creditOpeningFee", amounts.CreditOpeningFee);
    amountsData.Add("hiring", amounts.Hiring);
    amountsData.Add("owed", amounts.Owed);
    return amountsData;
  }

  public static Amounts FromData(Dictionary<string, object> data)
  {
    Amounts amounts = Activator.CreateInstance<Amounts>();

    amounts.GetType().GetProperty("BankSlip").SetValue(amounts, data["bankSlip"]);
    amounts.GetType().GetProperty("Iof").SetValue(amounts, data["iof"]);
    amounts.GetType().GetProperty("Installment").SetValue(amounts, data["installment"]);
    amounts.GetType().GetProperty("Insurance").SetValue(amounts, data["insurance"]);
    amounts.GetType().GetProperty("CreditOpeningFee").SetValue(amounts, data["creditOpeningFee"]);
    amounts.GetType().GetProperty("Hiring").SetValue(amounts, data["hiring"]);
    amounts.GetType().GetProperty("Owed").SetValue(amounts, data["owed"]);

    return amounts;
  }
}
