using AbrPlus.Integration.OpenERP.Api.DataContracts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Quote
{
    public class QuoteService : IQuoteService
    {
        private readonly IRahkaranCompanyService _sampleErpCompanyService;
        private readonly ILogger<QuoteService> _logger;

        public QuoteService(IRahkaranCompanyService sampleErpCompanyService,
                            ILogger<QuoteService> logger)
        {
            _sampleErpCompanyService = sampleErpCompanyService;
            _logger = logger;
        }

        public Task<string[]> GetAllIds()
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceBundle> GetBundle(string key)
        {
            throw new NotImplementedException();
        }

        public ChangeInfo GetChanges(string lastTrackedVersionStamp)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save(InvoiceBundle item)
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
