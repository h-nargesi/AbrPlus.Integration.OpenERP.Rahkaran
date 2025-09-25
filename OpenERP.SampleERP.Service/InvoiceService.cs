using AbrPlus.Integration.OpenERP.Api.DataContracts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AbrPlus.Integration.OpenERP.SampleERP.Dtos;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using Refit;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public class InvoiceService(ISession session, ILogger<InvoiceService> logger) : IInvoiceService
{
    public string[] GetAllIds()
    {
        throw new NotImplementedException();
    }

    public InvoiceBundle GetBundle(string key)
    {
        throw new NotImplementedException();
    }

    public ChangeInfo GetChanges(string lastTrackedVersionStamp)
    {
        throw new NotImplementedException();
    }

    public bool Save(InvoiceBundle item)
    {
        throw new NotImplementedException();
    }

    public void SetTrackingStatus(bool enabled)
    {
    }

    public bool SyncWithCrmObjectTypeCode()
    {
        return false;
    }

    public bool Validate(InvoiceBundle item)
    {
        throw new NotImplementedException();
    }
}

internal interface IInvoiceWebService
{
    [Get("/users/{user}")]
    Task<InvoiceDto> GetUser(string user);
}
