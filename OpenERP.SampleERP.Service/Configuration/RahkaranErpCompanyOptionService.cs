using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using AbrPlus.Integration.OpenERP.Settings;

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

            if (!int.TryParse(settings.RahkaranLoginIdleTimeout, out var idleTimeout))
            {
                idleTimeout = 0;
            }

            return new RahkaranErpCompanyConfig
            {
                ConnectionString = company.ConnecitonString,
                RahkaranWebServiceUrl = settings.RahkaranWebServiceUrl,
                RahkaranUsername = settings.RahkaranUsername,
                RahkaranPassword = settings.RahkaranPassword,
                IdleTimeout = idleTimeout,
            };
        }
    }
}
