using AbrPlus.Integration.OpenERP.Api.Contracts;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using AbrPlus.Integration.OpenERP.Api.DataContracts;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service
{
    public class InventoryService : IInventoryService
    {
        private readonly ISampleErpCompanyService _sampleErpCompanyService;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(ISampleErpCompanyService sampleErpCompanyService,
                                ILogger<InventoryService> logger)
        {
            _sampleErpCompanyService = sampleErpCompanyService;
            _logger = logger;
        }
        public List<RemainingStock> GetRemainingStock(string productCode)
        {
            var setting = _sampleErpCompanyService.GetCompanyConfig();

            throw new NotImplementedException();
        }
    }
}
