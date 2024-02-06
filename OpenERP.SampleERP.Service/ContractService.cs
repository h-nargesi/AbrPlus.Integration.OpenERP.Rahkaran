using AbrPlus.Integration.OpenERP.Api.DataContracts;
using Microsoft.Extensions.Logging;
using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service
{
    public class ContractService : IContractService
    {
        private readonly ISampleErpCompanyService _sampleErpCompanyService;
        private readonly ILogger<ContractService> _logger;

        public ContractService(ISampleErpCompanyService sampleErpCompanyService,
                              ILogger<ContractService> logger)
        {
            _sampleErpCompanyService = sampleErpCompanyService;
            _logger = logger;
        }

        public string[] GetAllIds()
        {
            throw new NotImplementedException();
        }

        public ContractBundle GetBundle(string key)
        {
            throw new NotImplementedException();
        }

        public ChangeInfo GetChanges(string lastTrackedVersionStamp)
        {
            throw new NotImplementedException();
        }

        public bool Save(ContractBundle item)
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

        public bool Validate(ContractBundle item)
        {
            throw new NotImplementedException();
        }
    }
}
