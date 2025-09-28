using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Quote;

public class QuoteService(ISession session, IQuotationSLS3Repository repository, ILogger<QuoteService> logger) : IQuoteService
{
    public Task<InvoiceBundle> GetBundle(string key)
    {
        throw new NotImplementedException();
    }

    public ChangeInfo GetChanges(string lastTrackedVersionStamp)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Save(InvoiceBundle item)
    {
        throw new NotImplementedException();
    }

    public void SetTrackingStatus(bool enabled)
    {
        throw new NotImplementedException();
    }

    public bool SyncWithCrmObjectTypeCode()
    {
        return false;
    }

    public Task<string[]> GetAllIds()
    {
        return repository.GetAllIdsAsync();
    }

    public bool Validate(InvoiceBundle item)
    {
        throw new NotImplementedException();
    }
}
