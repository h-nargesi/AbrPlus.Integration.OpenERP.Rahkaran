using AbrPlus.Integration.OpenERP.Api.DataContracts;
using Microsoft.Extensions.Logging;
using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly ISampleErpCompanyService _sampleErpCompanyService;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(ISampleErpCompanyService sampleErpCompanyService,
                              ILogger<PaymentService> logger)
        {
            _sampleErpCompanyService = sampleErpCompanyService;
            _logger = logger;
        }
        public PaymentBundle GetBundle(string key)
        {
            throw new NotImplementedException();
        }

        public ChangeInfo GetChanges(string lastTrackedVersionStamp)
        {
            throw new NotImplementedException();
        }

        public bool Save(PaymentBundle item)
        {
            throw new NotImplementedException();
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
