using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using AbrPlus.Integration.OpenERP.Service.Configuration;
using AbrPlus.Integration.OpenERP.Settings;
using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Configuration
{
    public class SampleErpCompanyOptionService : ISampleErpCompanyOptionService
    {
        private readonly ISampleErpCompanyOptionStorageService _sampleErpCompanyOptionStorageService;

        public SampleErpCompanyOptionService(ISampleErpCompanyOptionStorageService sampleErpCompanyOptionStorageService)
        {
            _sampleErpCompanyOptionStorageService = sampleErpCompanyOptionStorageService;
        }

        public SampleErpCompanyConfig GetCompanyFlatConfig(int companyId)
        {
            var company = _sampleErpCompanyOptionStorageService.GetCompanyConfig(companyId) ?? new CompanyConfig()/* empty company. */;
            var settings = _sampleErpCompanyOptionStorageService.GetFinancialSpecificModel(companyId);

            return new SampleErpCompanyConfig
            {
                ConnectionString = company.ConnecitonString,

                CheckBoxSetting = settings.CheckBoxSetting,
                DropdownSetting = settings.DropdownSetting,
                StringSetting = settings.StringSetting,
            };
        }
    }
}
