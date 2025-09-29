using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Dtos;
using AbrPlus.Integration.OpenERP.SampleERP.Models;
using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Service.Invoice;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class InvoiceServiceTest : BaseServiceTest
{
    //[Fact]
    //public void GetBundle_Test()
    //{
    //    using var session = GetSession(out _);
    //    var service = new InvoiceService(session, Utility.GetLogger<InvoiceService>());

    //}

    [Fact]
    public async Task Save_Test()
    {
        using var session = GetSession(out _);
        var service = new InvoiceService(session,
            GenerateRepository<InvoiceRmsRepository>(),
            Utility.GetLogger<InvoiceService>());

        var dto = new InvoiceRmsDto
        {
            DateTime = DateTime.Now,
            CustomerId = 1,
            CurrencyId = 1,
            SettlementPolicyId = 0,
            StoreId = 0,
            DocumentPatternId = 0,
            SalesAreaId = null,
            Price = 10,
            NetPrice = 10,
            CashierDiscount = null,
            Items = [],
            Policies = [],
            Description = null,
            CustomerAddressId = null,
            ReturnReasonId = null,
            SalesAgentId = null,
            LoyaltyProgramId = null
        };

        var result = await service.Save(dto.ToBundle(new IdentityBundle { Id = "1" }));

        Assert.True(result);
    }
}
