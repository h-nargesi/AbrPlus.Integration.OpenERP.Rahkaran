using AbrPlus.Integration.OpenERP.Api.Contracts;
using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Service;
using SeptaKit.DI;
using System.Collections.Generic;

namespace AbrPlus.Integration.OpenERP.SampleERP.Api.Api;

public class CustomerApi(ICustomerService customerService) : ICustomerApi, IApi
{
    public List<IdentityBalance> GetAllIdentityBalance(IdentityBalanceParams identityBalanceParams, int companyId)
    {
        return customerService.GetAllIdentityBalance(identityBalanceParams);
    }
    public string[] GetAllIds(int? companyId)
    {
        return customerService.GetAllIds().Result;
    }
    public IdentityBundle GetBundle(string key, int? companyId)
    {
        return customerService.GetBundle(key).Result;
    }
    public IdentityBundle GetBundleByCode(string key, int companyId)
    {
        return customerService.GetBundleByCode(key);
    }
    public ChangeInfo GetChanges(string lastTrackedVersion, int? companyId)
    {
        return customerService.GetChanges(lastTrackedVersion);
    }
    public decimal GetCustomerBalance(string customerCode, int companyId)
    {
        return customerService.GetCustomerBalance(customerCode);
    }
    public bool Save(IdentityBundle item, int? companyId)
    {
        return customerService.Save(item).Result;
    }
    public void SetTrackingStatus(bool enabled, int? companyId)
    {
        customerService.SetTrackingStatus(enabled);
    }
    public bool Validate(IdentityBundle item)
    {
        return customerService.Validate(item);
    }
}
