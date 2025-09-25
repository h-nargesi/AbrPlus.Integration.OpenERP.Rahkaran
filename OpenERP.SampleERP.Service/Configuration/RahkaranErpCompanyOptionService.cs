using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using AbrPlus.Integration.OpenERP.Service.Configuration;
using AbrPlus.Integration.OpenERP.Settings;
using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Configuration
{
    public class RahkaranErpCompanyOptionService(
        IRahkaranErpCompanyOptionStorageService rahkaranErpCompanyOptionStorageService)
        : IRahkaranErpCompanyOptionService
    {
        public RahkaranErpCompanyConfig GetCompanyFlatConfig(int companyId)
        {
            var company = rahkaranErpCompanyOptionStorageService.GetCompanyConfig(companyId) ?? new CompanyConfig()/* empty company. */;
            var settings = rahkaranErpCompanyOptionStorageService.GetFinancialSpecificModel(companyId);

            return new RahkaranErpCompanyConfig
            {
                ConnectionString = company.ConnecitonString,
                RahkaranWebServiceUrl = settings.RahkaranWebServiceUrl,
                RahkaranUsername = settings.RahkaranUsername,
                RahkaranPassword = settings.RahkaranPassword,
            };
        }
    }
}
