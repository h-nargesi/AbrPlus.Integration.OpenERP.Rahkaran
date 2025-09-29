using AbrPlus.Integration.OpenERP.Api.Contracts;
using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Service;
using SeptaKit.DI;

namespace AbrPlus.Integration.OpenERP.SampleERP.Api.Api;

public class QuoteApi(IQuoteService quoteService) : IQuoteApi, IApi
{
    public string[] GetAllIds(int? companyId)
    {
        return quoteService.GetAllIds().Result;
    }
    public InvoiceBundle GetBundle(string key, int? companyId)
    {
        return quoteService.GetBundle(key).Result;
    }
    public ChangeInfo GetChanges(string lastTrackedVersion, int? companyId)
    {
        return quoteService.GetChanges(lastTrackedVersion).Result;
    }
    public bool Save(InvoiceBundle item, int? companyId)
    {
        return quoteService.Save(item).Result;
    }
    public void SetTrackingStatus(bool enabled, int? companyId)
    {
        quoteService.SetTrackingStatus(enabled);
    }
    public bool SyncWithCrmObjectTypeCode(int companyId)
    {
        return quoteService.SyncWithCrmObjectTypeCode();
    }
    public bool Validate(InvoiceBundle item)
    {
        return quoteService.Validate(item);
    }
}
