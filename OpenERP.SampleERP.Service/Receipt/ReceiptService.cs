using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using Microsoft.Extensions.Logging;
using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Receipt
{
    public class ReceiptService(ISession session, ILogger<ReceiptService> logger) : IReceiptService
    {
        private const string BasePath = "/ReceiptAndPayment/ReceiptManagement/Services/ReceiptManagementService.svc";

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
                logger.LogError(ex, "Error in ReceiptService.GetBundle");
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
                var service = session.GetWebService<IReceiptWebService>(BasePath);

                var data = new
                {
                    PaymentData = bundle.ToDto()
                };

                var result = session.TryCall((token) => service.RegisterReceipt(data, token.Cookie)).Result;

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
            throw new NotImplementedException();
        }

        public string[] GetAllIds()
        {
            throw new NotImplementedException();
        }

        public void SetTrackingStatus(bool enabled)
        {

        }
    }
}
