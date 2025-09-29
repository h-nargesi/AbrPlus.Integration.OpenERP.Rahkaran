using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Invoice;

public class InvoiceService(ISession session, IInvoiceRmsRepository repository, ILogger<InvoiceService> logger)
    : IInvoiceService
{
    public async Task<InvoiceBundle> GetBundle(string key)
    {
        try
        {
            if (!long.TryParse(key, out var id))
            {
                // TODO use custom exception
                throw new Exception($"Invalid Invoice Key: {key}");
            }

            var service = session.GetWebService<IInvoiceRmsWebService>(IInvoiceRmsWebService.BasePath);

            var response = await session.TryCall((token) => service.GetInvoiceById(id, token.Cookie));

            if (response.Metadata?.ErrorMessage?.Length > 0)
            {
                // TODO use custom exception
                throw new Exception(response.Metadata?.ErrorMessage);
            }

            if (response?.Metadata?.StackTrace?.Length > 0)
            {
                var trace = response.Metadata.StackTrace;
                logger.LogError("Error in InvoiceService.GetBundle\n{trace}", trace);
            }

            return response.Result.ToBundle(null);
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

    public async Task<bool> Save(InvoiceBundle bundle)
    {
        try
        {
            var service = session.GetWebService<IInvoiceRmsWebService>(IInvoiceRmsWebService.BasePath);

            var data = new
            {
                document = bundle.ToDto()
            };

            var response = await session.TryCall((token) => service.SaveInvoice(data, token.Cookie));

            if (response.Metadata?.ErrorMessage?.Length > 0)
            {
                // TODO use custom exception
                throw new Exception(response.Metadata?.ErrorMessage);
            }

            if (response?.Metadata?.StackTrace?.Length > 0)
            {
                var trace = response.Metadata.StackTrace;
                logger.LogError("Error in InvoiceService.Save\n{trace}", trace);
            }

            return response.Result != null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in InvoiceService.Save");
            throw;
        }
    }

    public bool Validate(InvoiceBundle item)
    {
        throw new NotSupportedException("Invoice validation is not supported in Rahkaran.");
    }

    public Task<string[]> GetAllIds()
    {
        return repository.GetAllIdsAsync();
    }

    public void SetTrackingStatus(bool enabled)
    {
        throw new NotImplementedException();
    }

    public bool SyncWithCrmObjectTypeCode()
    {
        throw new NotImplementedException();
    }
}
