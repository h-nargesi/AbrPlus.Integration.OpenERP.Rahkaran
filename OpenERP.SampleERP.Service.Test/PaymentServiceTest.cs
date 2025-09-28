using AbrPlus.Integration.OpenERP.SampleERP.Dtos;
using AbrPlus.Integration.OpenERP.SampleERP.Service.Payment;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class PaymentServiceTest : BaseServiceTest
{
    [Fact]
    public void Save_Test()
    {
        using var session = GetSession(out _);
        var service = new PaymentService(session, Utility.GetLogger<PaymentService>());

        var payment = new PaymentDto
        {
            BranchID = 1,
            Date = DateTime.Now,
            PaymentCashMoneys = [ new() {
                Amount = 100,
                CashFlowFactorID = 1,
            } ]
        };

        var result = service.Save(payment.ToBundle());

        Assert.True(result);
    }
}
