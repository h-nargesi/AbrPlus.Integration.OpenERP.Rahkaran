using AbrPlus.Integration.OpenERP.Api.DataContracts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AbrPlus.Integration.OpenERP.SampleERP.Dtos;
using AbrPlus.Integration.OpenERP.SampleERP.Options;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using Microsoft.Extensions.Options;
using Refit;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public class InvoiceService(ISession session, IOptions<RahkaranUrlInfo> options, ILogger<InvoiceService> logger)
    : IInvoiceService
{
    private const string BasePath = "services/sales/invoices";
    
    public string[] GetAllIds()
    {
        throw new NotImplementedException();
    }

    public InvoiceBundle GetBundle(string key)
    {
        var service = GetRestService();
        var result = session.TryCall((token) => service.Get("1235654", token.Cookie)).Result;

        return new InvoiceBundle
        {
            Id = result.Id,
            Subject = result.Subject,
            Description = result.Description,
            Customer = result.Customer,
            Number = result.Number,
            Discount = result.Discount,
            ExpireDate = result.ExpireDate,
            InvoiceDate = result.InvoiceDate,
            InvoiceType = result.InvoiceType,
            PriceListName = result.PriceListName,
            Tax = result.Tax,
            Toll = result.Toll,
            TaxPercent = result.TaxPercent,
            TollPercent = result.TollPercent,
            TotalValue = result.TotalValue,
            Details = result.Details,
            Payments = result.Payments,
            FinalValue = result.FinalValue,
            SubInvoiceId = result.SubInvoiceId,
            SubInvoiceType = result.SubInvoiceType,
            AdditionalCosts = result.AdditionalCosts,
            CrmObjectTypeCode = result.CrmObjectTypeCode,
            Creator = result.Creator,
        };
    }

    public ChangeInfo GetChanges(string lastTrackedVersionStamp)
    {
        throw new NotImplementedException();
    }

    public bool Save(InvoiceBundle invoice)
    {
        var bundle = new InvoiceDto
        {
            Id = invoice.Id,
            Subject = invoice.Subject,
            Description = invoice.Description,
            Customer = invoice.Customer,
            Number = invoice.Number,
            Discount = invoice.Discount,
            ExpireDate = invoice.ExpireDate,
            InvoiceDate = invoice.InvoiceDate,
            InvoiceType = invoice.InvoiceType,
            PriceListName = invoice.PriceListName,
            Tax = invoice.Tax,
            Toll = invoice.Toll,
            TaxPercent = invoice.TaxPercent,
            TollPercent = invoice.TollPercent,
            TotalValue = invoice.TotalValue,
            Details = invoice.Details,
            Payments = invoice.Payments,
            FinalValue = invoice.FinalValue,
            SubInvoiceId = invoice.SubInvoiceId,
            SubInvoiceType = invoice.SubInvoiceType,
            AdditionalCosts = invoice.AdditionalCosts,
            CrmObjectTypeCode = invoice.CrmObjectTypeCode,
            Creator = invoice.Creator,
        };
        
        var service = GetRestService();
        session.TryCall((token) => service.Save(bundle, token.Cookie)).Wait();

        return true;
    }

    public void SetTrackingStatus(bool enabled)
    {
        throw new NotImplementedException();
    }

    public bool SyncWithCrmObjectTypeCode()
    {
        throw new NotImplementedException();
    }

    public bool Validate(InvoiceBundle item)
    {
        throw new NotImplementedException();
    }

    private IInvoiceWebService GetRestService()
    {
        var baseUrl = options.Value.BaseUrl;
        if (baseUrl.EndsWith('/')) baseUrl = baseUrl[..^1];
        return RestService.For<IInvoiceWebService>($"{baseUrl}/{BasePath}");
    }
}

internal interface IInvoiceWebService
{
    [Get("get")]
    Task<InvoiceDto> Get(string key, [Header("Cookie")] string cookie);
    
    [Get("save")]
    Task<object> Save(InvoiceDto item, [Header("Cookie")] string cookie);
}