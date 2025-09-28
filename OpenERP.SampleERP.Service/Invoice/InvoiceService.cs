using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Invoice;

public class InvoiceService(ISession session, IInvoiceSLS3Repository repository, ILogger<InvoiceService> logger)
    : IInvoiceService
{
    private const string BasePathRms = "Retail/eSalesApi/ESalesService.svc";
    private const string BasePathSls = "Services/Sales/InvoiceManagementService.svc";

    public async Task<InvoiceBundle> GetBundle(string key)
    {
        try
        {
            if (!long.TryParse(key, out var id))
            {
                // TODO use custom exception
                throw new Exception($"Invalid Invoice Key: {key}");
            }

            var service = session.GetWebService<IInvoiceWebService>(BasePathRms);

            var dto = await session.TryCall((token) => service.GetInvoiceById(new { id }, token.Cookie));

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
            var service = session.GetWebService<IInvoiceWebService>(BasePathRms);

            var data = new
            {
                document = bundle.ToDto()
            };

            var result = await session.TryCall((token) => service.SaveInvoice(data, token.Cookie));

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
