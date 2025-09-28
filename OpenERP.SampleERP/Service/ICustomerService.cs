using AbrPlus.Integration.OpenERP.Api.DataContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public interface ICustomerService
{
    Task<bool> Save(IdentityBundle item);
    ChangeInfo GetChanges(string lastTrackedVersion);
    Task<IdentityBundle> GetBundle(string key);
    bool Validate(IdentityBundle item);
    void SetTrackingStatus(bool enabled);
    Task<string[]> GetAllIds();
    IdentityBundle GetBundleByCode(string key);
    decimal GetCustomerBalance(string customerCode);
    List<IdentityBalance> GetAllIdentityBalance(IdentityBalanceParams identityBalanceParams);
}
