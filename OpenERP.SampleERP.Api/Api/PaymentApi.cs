using AbrPlus.Integration.OpenERP.Api.Contracts;
using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Service;
using SeptaKit.DI;

namespace AbrPlus.Integration.OpenERP.SampleERP.Api.Api;

public class PaymentApi(IPaymentService paymentService) : IPaymentApi, IApi
{
    public string[] GetAllIds(int? companyId)
    {
        return paymentService.GetAllIds().Result;
    }
    public PaymentBundle GetBundle(string key, int? companyId)
    {
        return paymentService.GetBundle(key).Result;
    }
    public ChangeInfo GetChanges(string lastTrackedVersion, int? companyId)
    {
        return paymentService.GetChanges(lastTrackedVersion).Result;
    }
    public bool Save(PaymentBundle item, int? companyId)
    {
        return paymentService.Save(item).Result;
    }
    public void SetTrackingStatus(bool enabled, int? companyId)
    {
        paymentService.SetTrackingStatus(enabled);
    }
    public bool Validate(PaymentBundle item)
    {
        return paymentService.Validate(item);
    }
}
