using AbrPlus.Integration.OpenERP.Api.DataContracts;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public interface IInvoiceService
{
    Task<string[]> GetAllIds();
    Task<InvoiceBundle> GetBundle(string key);
    bool Validate(InvoiceBundle item);
    Task<bool> Save(InvoiceBundle invoice);
    void SetTrackingStatus(bool enabled);
    bool SyncWithCrmObjectTypeCode();
    Task<ChangeInfo> GetChanges(string lastTrackedVersionStamp);
}
