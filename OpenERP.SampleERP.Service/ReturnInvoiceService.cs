using AbrPlus.Integration.OpenERP.Api.DataContracts;
using Microsoft.Extensions.Logging;
using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service
{
    public class ReturnInvoiceService : IReturnInvoiceService
    {
        private readonly IRahkaranCompanyService _sampleErpCompanyService;
        private readonly ILogger<ReturnInvoiceService> _logger;

        public ReturnInvoiceService(IRahkaranCompanyService sampleErpCompanyService,
                                    ILogger<ReturnInvoiceService> logger)
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
