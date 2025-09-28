using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using AbrPlus.Integration.OpenERP.Settings;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Configuration
{
    public class RahkaranErpCompanyOptionService(
        IRahkaranErpCompanyOptionStorageService rahkaranErpCompanyOptionStorageService)
        : IRahkaranCompanyOptionService
    {
        public RahkaranCompanyConfig GetCompanyFlatConfig(int companyId)
        {
            var company = rahkaranErpCompanyOptionStorageService.GetCompanyConfig(companyId) ?? new CompanyConfig()/* empty company. */;
            var settings = rahkaranErpCompanyOptionStorageService.GetFinancialSpecificModel(companyId);

            if (!int.TryParse(settings.RahkaranLoginIdleTimeout, out var idleTimeout))
            {
                idleTimeout = 0;
            }

            var rakaranWebServiceUrl = settings.RahkaranWebServiceUrl;
            if (rakaranWebServiceUrl != null && rakaranWebServiceUrl.EndsWith('/')) rakaranWebServiceUrl = rakaranWebServiceUrl[..^1];

            return new RahkaranCompanyConfig
            {
                ConnectionString = company.ConnecitonString,
                BaseUrl = rakaranWebServiceUrl,
                Username = settings.RahkaranUsername,
                Password = settings.RahkaranPassword,
                IdleTimeout = idleTimeout,
            };
        }
    }
}
