using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using AbrPlus.Integration.OpenERP.SampleERP.Shared;
using Microsoft.Extensions.Logging;
using Refit;
using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Invoice;

public class InvoiceService(ISession session, ISampleErpCompanyService company, ILogger<InvoiceService> logger)
    : IInvoiceService
{
    private const string BasePath = "Retail/eSalesApi/ESalesService.svc";
    private readonly RahkaranErpCompanyConfig config = company.GetCompanyConfig();

    public string[] GetAllIds()
    {
        throw new NotImplementedException();
    }

    public InvoiceBundle GetBundle(string key)
    {
        try
        {
            if (!long.TryParse(key, out var id))
            {
                // TODO use custom exception
                throw new Exception($"Invlid Invoice Key: {key}");
            }

            var service = GetRestService();

            var dto = session.TryCall((token) => service.GetInvoiceById(new { id }, token.Cookie)).Result;

            return dto.GetInvoiceByIdResult.ToBundle(null);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in InvoiceService.GetBundle");
            throw;
        }
    }

    public ChangeInfo GetChanges(string lastTrackedVersionStamp)
    {
        throw new NotImplementedException();
    }

    public bool Save(InvoiceBundle bundle)
    {
        try
        {
            var service = GetRestService();

            var data = new InvoiceSaveDocument
            {
                document = bundle.ToDto()
            };

            var result = session.TryCall((token) => service.SaveInvoice(data, token.Cookie)).Result;

            //var messages = result?.FirstOrDefault()?.ValidationErrors;

            //if (messages?.Length > 0)
            //{
            //    // TODO use custom exception
            //    throw new Exception(string.Join('\n', messages));
            //}

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in InvoiceService.Save");
            throw;
        }
    }

    public void SetTrackingStatus(bool enabled)
    {
        throw new NotImplementedException();
    }

    public bool SyncWithCrmObjectTypeCode()
    {
        throw new NotImplementedException();
    }

    public bool Validate(InvoiceBundle item)
    {
        throw new NotImplementedException();
    }

    private IInvoiceWebService GetRestService()
    {
        return RestService.For<IInvoiceWebService>(config.BaseUrl + BasePath, JsonObjectExtension.RefitSettings);
    }
}
