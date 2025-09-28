using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Payment;

public class PaymentService(ISession session, IPaymentRepository repository, ILogger<PaymentService> logger) : IPaymentService
{
    private const string BasePath = "/ReceiptAndPayment/PaymentManagement/Services/PaymentManagementService.svc";
    
    public Task<PaymentBundle> GetBundle(string key)
    {
        try
        {
            if (!long.TryParse(key, out var id))
            {
                // TODO use custom exception
                throw new Exception($"Invalid Invoice Key: {key}");
            }

            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in PaymentService.GetBundle");
            throw;
        }
    }

    public ChangeInfo GetChanges(string lastTrackedVersionStamp)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Save(PaymentBundle bundle)
    {
        try
        {
            var service = session.GetWebService<IPaymentWebService>(BasePath);

            var dto = bundle.ToDto();

            var result = await session.TryCall((token) => service.RegisterPayment(dto, token.Cookie));

            var messages = result?.ValidationErrors;

            if (messages?.Length > 0)
            {
                // TODO use custom exception
                throw new Exception(string.Join('\n', messages));
            }

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in InvoiceService.Save");
            throw;
        }
    }

    public bool Validate(PaymentBundle item)
    {
        throw new NotSupportedException();
    }

    public Task<string[]> GetAllIds()
    {
        return repository.GetAllIdsAsync();
    }

    public void SetTrackingStatus(bool enabled)
    {
        throw new NotImplementedException();
    }
}
