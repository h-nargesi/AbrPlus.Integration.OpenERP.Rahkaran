using AbrPlus.Integration.OpenERP.SampleERP.Service.Invoice;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class InvoiceServiceTest : BaseServiceTest
{
    [Fact]
    public void GetBundle_Test()
    {
        using var session = GetSession(out _);
        var service = new InvoiceService(session, Company.Object, Utility.GetLogger<InvoiceService>());

    }

    [Fact]
    public async Task Save_Test()
    {
        using var session = GetSession(out _);
        var service = new InvoiceService(session, Company.Object, Utility.GetLogger<InvoiceService>());
    }
}
