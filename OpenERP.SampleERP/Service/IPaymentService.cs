using AbrPlus.Integration.OpenERP.Api.DataContracts;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public interface IPaymentService
{
    Task<string[]> GetAllIds();
    Task<PaymentBundle> GetBundle(string key);
    bool Validate(PaymentBundle item);
    Task<bool> Save(PaymentBundle item);
    void SetTrackingStatus(bool enabled);
    Task<ChangeInfo> GetChanges(string lastTrackedVersionStamp);
}
