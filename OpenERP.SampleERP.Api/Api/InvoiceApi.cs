using AbrPlus.Integration.OpenERP.Api.Contracts;
using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Service;
using SeptaKit.DI;

namespace AbrPlus.Integration.OpenERP.SampleERP.Api.Api;

public class InvoiceApi(IInvoiceService invoiceService) : IInvoiceApi, IApi
{
    public string[] GetAllIds(int? companyId)
    {
        return invoiceService.GetAllIds().Result;
    }

    public InvoiceBundle GetBundle(string key, int? companyId)
    {
        return invoiceService.GetBundle(key).Result;
    }

    public ChangeInfo GetChanges(string lastTrackedVersion, int? companyId)
    {
        return invoiceService.GetChanges(lastTrackedVersion).Result;
    }

    public bool Save(InvoiceBundle item, int? companyId)
    {
        return invoiceService.Save(item).Result;
    }

    public void SetTrackingStatus(bool enabled, int? companyId)
    {
        invoiceService.SetTrackingStatus(enabled);
    }

    public bool SyncWithCrmObjectTypeCode(int companyId)
    {
        return invoiceService.SyncWithCrmObjectTypeCode();
    }

    public bool Validate(InvoiceBundle item)
    {
        return invoiceService.Validate(item);
    }
}

