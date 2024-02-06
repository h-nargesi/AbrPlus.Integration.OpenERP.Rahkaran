using AbrPlus.Integration.OpenERP.Helpers;
using AbrPlus.Integration.OpenERP.Options;
using AbrPlus.Integration.OpenERP.SampleERP.Enums;
using AbrPlus.Integration.OpenERP.SampleERP.Service.Configuration;
using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using AbrPlus.Integration.OpenERP.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service
{
    public class SampleErpCompanyService : ISampleErpCompanyService
    {
        private readonly ICompanyContext _companyContext;
        private readonly ISampleErpCompanyOptionService _sampleErpCompanyOptionService;
        private readonly AppOption _appOptions;
        private readonly ILogger<SampleErpCompanyService> _logger;

        public SampleErpCompanyService(ICompanyContext companyContext,
                                      ISampleErpCompanyOptionService sampleErpCompanyOptionService,
                                      IOptions<AppOption> options,
                                      ILogger<SampleErpCompanyService> logger)
        {
            _companyContext = companyContext;
            _sampleErpCompanyOptionService = sampleErpCompanyOptionService;
            _appOptions = options.Value;
            _logger = logger;
        }


        public SampleErpCompanyConfig GetCompanyConfig()
        {
            return _sampleErpCompanyOptionService.GetCompanyFlatConfig(_companyContext.CompanyId);
        }
        public string GetCurrentVersion()
        {
            return "1.0.0";
        }
        public bool IsCurrentVersionCompatible()
        {
            try
            {
                CheckVersionIsCompatible();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void CheckVersionIsCompatible()
        {
            GetCompatibleVersion();
        }
        public bool TryGetCompatibleVersion(out SampleErpVersion compatibleVersion, out string currentVersion)
        {
            currentVersion = GetCurrentVersion();
            try
            {
                compatibleVersion = GetCompatibleVersion();
                return true;
            }
            catch
            {
                compatibleVersion = SampleErpVersion.None;
                return false;
            }
        }

        private SampleErpVersion GetCompatibleVersion()
        {
            try
            {
                var version = GetCurrentVersion();
                _logger.LogDebug($"Instantiating SampleErp repository version {version} for company {_companyContext.CompanyId} ...");

                version = "V" + version.Replace('.', '_');
                var releaseVersion = version.Substring(0, version.LastIndexOf('_'));
                var majorVersion = version.Substring(0, version.IndexOf('_'));

                SampleErpVersion sampleErpVersion = SampleErpVersion.None;
                SampleErpVersion sampleErpLastVersion = SampleErpVersion.V2_0_0;

                if (version.IsBiggerVersion(sampleErpLastVersion.ToString()))
                {
                    if (_appOptions.UseLatestVersion)
                    {
                        _logger.LogDebug("Attempting to use latest repository version.");
                        sampleErpVersion = sampleErpLastVersion;
                    }
                    else
                    {
                        throw new Exception(string.Format("ورژن جاری شرکت نمونه {0} میباشد و با ورژن همگام ساز مرتبط نیست. لطفا سرویس همگام ساز را بروز رسانی کنید.", version));
                    }
                }
                else
                {
                    if (!Enum.TryParse(version, true, out sampleErpVersion))
                    {
                        if (!Enum.TryParse(releaseVersion, true, out sampleErpVersion))
                        {
                            if (!Enum.TryParse(majorVersion, true, out sampleErpVersion))
                            {

                            }
                        }
                    }
                }

                if (sampleErpVersion != SampleErpVersion.None)
                {
                    return sampleErpVersion;
                }
                else
                {
                    throw new Exception(string.Format("ورژن جاری شرکت نمونه {0} میباشد و با ورژن همگام ساز مرتبط نیست. لطفا سرویس همگام ساز را بروز رسانی کنید.", version));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in IsCurrentVersionCompatible");
                throw;
            }
        }


    }
}
