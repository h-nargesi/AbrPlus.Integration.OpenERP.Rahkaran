using AbrPlus.Integration.OpenERP.Api.DataContracts;
using Microsoft.Extensions.Logging;
using System;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Payment;

public class PaymentService(ISession session, ILogger<PaymentService> logger) : IPaymentService
{
    private const string BasePath = "";
    
    public string[] GetAllIds()
    {
        throw new NotImplementedException();
    }

    public PaymentBundle GetBundle(string key)
    {
        try
        {
            if (!long.TryParse(key, out var id))
            {
                // TODO use custom exception
                throw new Exception($"Invalid Invoice Key: {key}");
            }

            var service = session.GetWebService<IPaymentWebService>(BasePath);

            var dto = session.TryCall((token) => service.GetPaymentById(new { id }, token.Cookie)).Result;

            return dto.Data.ToBundle();
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
                document = bundle.ToDto()
            };

            var result = session.TryCall((token) => service.SavePayment(data, token.Cookie)).Result;

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

    public bool Validate(PaymentBundle item)
    {
        throw new NotImplementedException();
    }

    public void SetTrackingStatus(bool enabled)
    {
        throw new NotImplementedException();
    }
}
