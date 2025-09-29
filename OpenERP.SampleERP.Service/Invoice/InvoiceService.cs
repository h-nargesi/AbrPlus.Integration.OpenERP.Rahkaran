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

            var dto = await session.TryCall((token) => service.GetInvoiceById(id, token.Cookie));

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

    public async Task<bool> Save(InvoiceBundle bundle)
    {
        try
        {
            var service = session.GetWebService<IInvoiceRmsWebService>(IInvoiceRmsWebService.BasePath);

            var data = new
            {
                document = bundle.ToDto()
            };

            var result = await session.TryCall((token) => service.SaveInvoice(data, token.Cookie));

            if (result.Metadata?.ErrorMessage?.Length > 0)
            {
                // TODO use custom exception
                throw new Exception(result.Metadata?.ErrorMessage);
            }

            if (result?.Metadata?.StackTrace?.Length > 0)
            {
                var trace = result.Metadata.StackTrace;
                logger.LogError("Error in InvoiceService.Save\n{trace}", trace);
            }

            return result.Result != null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in InvoiceService.Save");
            throw;
        }
    }

    public bool Validate(InvoiceBundle item)
    {
        throw new NotImplementedException();
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
