using AbrPlus.Integration.OpenERP.Api.DataContracts;
using Microsoft.Extensions.Logging;
using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service
{
    public class QuoteService : IQuoteService
    {
        private readonly ISampleErpCompanyService _sampleErpCompanyService;
        private readonly ILogger<QuoteService> _logger;

        public QuoteService(ISampleErpCompanyService sampleErpCompanyService,
                            ILogger<QuoteService> logger)
        {
            _sampleErpCompanyService = sampleErpCompanyService;
            _logger = logger;
        }

        public string[] GetAllIds()
        {
            throw new NotImplementedException();
        }

        public InvoiceBundle GetBundle(string key)
        {
            throw new NotImplementedException();
        }

        public ChangeInfo GetChanges(string lastTrackedVersionStamp)
        {
            throw new NotImplementedException();
        }

        public bool Save(InvoiceBundle item)
        {
            throw new NotImplementedException();
        }

        public void SetTrackingStatus(bool enabled)
        {
            throw new NotImplementedException();
        }

        public bool SyncWithCrmObjectTypeCode()
        {
            return false;
        }

        public bool Validate(InvoiceBundle item)
        {
            throw new NotImplementedException();
        }
    }
}
