using Autofac;
using Microsoft.Extensions.Logging;
using System;
using AbrPlus.Integration.OpenERP.SampleERP.Service.Configuration;
using AbrPlus.Integration.OpenERP.SampleERP.Enums;
using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.Settings;
using AbrPlus.Integration.OpenERP.Helpers;
using AbrPlus.Integration.OpenERP.Service.Configuration;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service
{
    public class SettingService : ISettingService
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly ISampleErpCompanySettingService _sampleErpCompanySettingService;
        private readonly IRahkaranErpCompanyOptionStorageService _rahkaranErpCompanyOptionStorageService;
        private readonly IConnectionStringValidator _connectionStringValidator;
        private readonly ILogger<SettingService> _logger;
        public SettingService(ILifetimeScope lifetimeScope,
                               ISampleErpCompanySettingService sampleErpCompanySettingService,
                               IRahkaranErpCompanyOptionStorageService rahkaranErpCompanyOptionStorageService,
                               IConnectionStringValidator connectionStringValidator,
                               ILogger<SettingService> logger)
        {
            _lifetimeScope = lifetimeScope;
            _sampleErpCompanySettingService = sampleErpCompanySettingService;
            _rahkaranErpCompanyOptionStorageService = rahkaranErpCompanyOptionStorageService;
            _connectionStringValidator = connectionStringValidator;
            _logger = logger;
        }
        public SystemInfoBundle GetInfo(int companyId)
        {
            using (var scope = _lifetimeScope.BeginLifetimeScopeForCompany(companyId))
            {
                var companyService = scope.Resolve<IRahkaranCompanyService>();
                var systemInfo = new SystemInfoBundle
                {
                    Name = "نرم افزار شرکت نمونه",

                };
                if (companyService.TryGetCompatibleVersion(out RahkaranVersion sampleErpVersion, out string currentVersion))
                {
                    systemInfo.Version = currentVersion;
                    systemInfo.VersionIsSupported = true;
                }
                else
                {
                    systemInfo.Version = currentVersion;
                    systemInfo.VersionIsSupported = false;
                }
                return systemInfo;
            }
        }

        public SettingsTestResult TestFinancialSystemSettings(FinancialSystemSettings settings)
        {
            try
            {
                var message = string.Empty;
                var result = _connectionStringValidator.Validate(settings.DatabaseAddress, settings.UseWindowsCredential, settings.DatabaseUsername, settings.DatabasePassword, out message);

                return new SettingsTestResult
                {
                    Success = result,
                    ErrorMessage = message
                };
            }
            catch (Exception ex)
            {
                return new SettingsTestResult
                {
                    ErrorMessage = ex.Message,
                    Success = false
                };
            }
        }
        public void DeleteFinancialSystem()
        {
            _rahkaranErpCompanyOptionStorageService.RemoveFinancialSystem();
        }
        public FinancialSystemConfig GetFinancialSystemConfig()
        {
            return _rahkaranErpCompanyOptionStorageService.GetConfig();
        }
        public void SetFinancialSystemConfig(FinancialSystemConfig config)
        {
            _rahkaranErpCompanyOptionStorageService.SetConfig(config);
        }
        public CompanyConfig GetCompanyConfig(int companyId)
        {
            return _rahkaranErpCompanyOptionStorageService.GetCompanyConfig(companyId);
        }
        public void SetCompanyConfig(CompanyConfig companyConfig)
        {
            _rahkaranErpCompanyOptionStorageService.SetCompanyConfig(companyConfig);
        }
        public void DeleteCompany(int companyId)
        {
            _rahkaranErpCompanyOptionStorageService.DeleteCompany(companyId);
        }
        public FinancialSystemSpecificConfig[] GetFinancialSystemSpecificConfigs(int companyId)
        {
            return _sampleErpCompanySettingService.GetFinancialSystemSpecificConfigs(companyId);
        }
        public void SetFinancialSystemSpecificConfigs(int companyId, FinancialSystemSpecificConfig[] configs)
        {
            if (configs == null || configs.Length == 0)
                return;

            _sampleErpCompanySettingService.SetFinancialSystemSpecificConfigs(companyId, configs);
        }
        public int GetCurrentFiscalYear(int companyId)
        {
            using (var scope = _lifetimeScope.BeginLifetimeScopeForCompany(companyId))
            {
                throw new NotImplementedException();
            }
        }
    }
}
