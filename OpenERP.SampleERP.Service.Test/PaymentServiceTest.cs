using AbrPlus.Integration.OpenERP.SampleERP.Dtos;
using AbrPlus.Integration.OpenERP.SampleERP.Models;
using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Service.Payment;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class PaymentServiceTest : BaseServiceTest
{
    [Fact]
    public async Task Save_Test()
    {
        using var session = GetSession(out _);
        var service = new PaymentService(session, 
            GenerateRepository<PaymentRepository>(), 
            Utility.GetLogger<PaymentService>());

        var payment = new PaymentDto
        {
            BranchID = 2,
            Date = DateTime.Now,
            CashID = 2,
            CounterPartDLCode = "3100200",
            PaymentCashMoneys = [ new() {
                Amount = 100,
                CounterPartDLCode = "3100200",
                AccountingOperationID = 47,
                CashFlowFactorID = 568,
                CurrencyAbbreviation = "IRR",
                OperationalCurrencyExchangeRate = 1,
                BaseCurrencyAbbreviation = "IRR",
                BaseCurrencyExchangeRate = 1,
            } ]
        };

        var result = await service.Save(payment.ToBundle());

        Assert.True(result);
    }
}
