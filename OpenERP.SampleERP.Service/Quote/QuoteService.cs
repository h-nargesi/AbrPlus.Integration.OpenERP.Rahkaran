using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Quote;

public class QuoteService(ISession session, IQuotationSlsRepository repository, ILogger<QuoteService> logger) 
    : ServiceBase(repository), IQuoteService
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

            var service = session.GetWebService<IQuoteRmsWebService>(IQuoteRmsWebService.BasePath);

            var response = await session.TryCall((token) => service.GetSalesOrderById(id, token.Cookie));

            if (response.Metadata?.ErrorMessage?.Length > 0)
            {
                // TODO use custom exception
                throw new Exception(response.Metadata?.ErrorMessage);
            }

            if (response?.Metadata?.StackTrace?.Length > 0)
            {
                var trace = response.Metadata.StackTrace;
                logger.LogError("Error in QuoteService.GetBundle\n{trace}", trace);
            }

            return response.Result.ToBundle(null);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in QuoteService.GetBundle");
            throw;
        }
    }

    public bool Validate(InvoiceBundle item)
    {
        throw new NotSupportedException("Quotation validation is not supported in Rahkaran.");
    }

    public async Task<bool> Save(InvoiceBundle bundle)
    {
        try
        {
            var service = session.GetWebService<IQuoteRmsWebService>(IQuoteRmsWebService.BasePath);

            var data = new
            {
                document = bundle.ToDto()
            };

            var response = await session.TryCall((token) => service.SaveSalesOrder(data, token.Cookie));

            if (response.Metadata?.ErrorMessage?.Length > 0)
            {
                // TODO use custom exception
                throw new Exception(response.Metadata?.ErrorMessage);
            }

            if (response?.Metadata?.StackTrace?.Length > 0)
            {
                var trace = response.Metadata.StackTrace;
                logger.LogError("Error in QuoteService.Save\n{trace}", trace);
            }

            return response.Result != null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in QuoteService.Save");
            throw;
        }
    }

    public void SetTrackingStatus(bool enabled)
    {
        throw new NotImplementedException();
    }

    public bool SyncWithCrmObjectTypeCode()
    {
        return false;
    }
}
