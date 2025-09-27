using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using Microsoft.Extensions.Logging;
using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Payment;

public class PaymentService(ISession session, ILogger<PaymentService> logger) : IPaymentService
{
    private const string BasePath = "/ReceiptAndPayment/PaymentManagement/Services/PaymentManagementService.svc";
    
    public PaymentBundle GetBundle(string key)
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

    public bool Save(PaymentBundle bundle)
    {
        try
        {
            var service = session.GetWebService<IPaymentWebService>(BasePath);

            var data = new
            {
                PaymentData = bundle.ToDto()
            };

            var result = session.TryCall((token) => service.RegisterPayment(data, token.Cookie)).Result;

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

    public string[] GetAllIds()
    {
        throw new NotImplementedException();
    }

    public void SetTrackingStatus(bool enabled)
    {
        throw new NotImplementedException();
    }
}
