using AbrPlus.Integration.OpenERP.Api.DataContracts;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public interface IQuoteService
{
    Task<string[]> GetAllIds();
    Task<InvoiceBundle> GetBundle(string key);
    ChangeInfo GetChanges(string lastTrackedVersionStamp);
    Task<bool> Save(InvoiceBundle item);
    void SetTrackingStatus(bool enabled);
    bool SyncWithCrmObjectTypeCode();
    bool Validate(InvoiceBundle item);
}
