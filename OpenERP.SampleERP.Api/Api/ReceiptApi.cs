using AbrPlus.Integration.OpenERP.Api.Contracts;
using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Service;
using SeptaKit.DI;

namespace AbrPlus.Integration.OpenERP.SampleERP.Api.Api;

public class ReceiptApi(IReceiptService receiptService) : IReceiptApi, IApi
{
    public string[] GetAllIds(int? companyId)
    {
        return receiptService.GetAllIds().Result;
    }
    public PaymentBundle GetBundle(string key, int? companyId)
    {
        return receiptService.GetBundle(key).Result;
    }
    public ChangeInfo GetChanges(string lastTrackedVersion, int? companyId)
    {
        return receiptService.GetChanges(lastTrackedVersion).Result;
    }
    public bool Save(PaymentBundle item, int? companyId)
    {
        return receiptService.Save(item).Result;
    }
    public void SetTrackingStatus(bool enabled, int? companyId)
    {
        receiptService.SetTrackingStatus(enabled);
    }
    public bool Validate(PaymentBundle item)
    {
        return receiptService.Validate(item);
    }
}
