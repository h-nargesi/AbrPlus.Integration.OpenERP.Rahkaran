using AbrPlus.Integration.OpenERP.Api.DataContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public interface ICustomerService
{
    Task<string[]> GetAllIds();
    Task<IdentityBundle> GetBundle(string key);
    IdentityBundle GetBundleByCode(string key);
    decimal GetCustomerBalance(string customerCode);
    List<IdentityBalance> GetAllIdentityBalance(IdentityBalanceParams identityBalanceParams);
    bool Validate(IdentityBundle item);
    Task<bool> Save(IdentityBundle item);
    void SetTrackingStatus(bool enabled);
    Task<ChangeInfo> GetChanges(string lastTrackedVersion);
}
