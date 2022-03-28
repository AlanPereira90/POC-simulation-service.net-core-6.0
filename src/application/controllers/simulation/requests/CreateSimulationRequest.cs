namespace src.application.controllers.simulation.requests;

public record CresteSimulationRequest(
    double Amount,
    PlanRequest Plan
);

public record PlanRequest(
    int Installments,
    RateRequest Rate,
    ValueRequest Value
);

public record RateRequest(
    CostsRequest TotalEffectiveCost,
    CostsRequest Interest,
    int Iof
);

public record CostsRequest(
    double Monthly,
    double Annual
);

public record ValueRequest(
    double BankSlip,
    double Iof,
    double Installment,
    double Insurance,
    double CreditOpeningFee,
    double Hiring,
    double Owed
);