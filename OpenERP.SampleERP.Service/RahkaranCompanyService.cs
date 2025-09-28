using AbrPlus.Integration.OpenERP.Helpers;
using AbrPlus.Integration.OpenERP.Options;
using AbrPlus.Integration.OpenERP.SampleERP.Enums;
using AbrPlus.Integration.OpenERP.SampleERP.Options;
using AbrPlus.Integration.OpenERP.SampleERP.Service.Configuration;
using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using AbrPlus.Integration.OpenERP.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SeptaKit.Repository;
using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public class RahkaranCompanyService(ICompanyContext companyContext,
    IRahkaranCompanyOptionService rahkaranErpCompanyOptionService,
    IOptions<AppOption> options,
    IOptions<RahkaranUrlInfo> rahkaranOptions,
    ILogger<RahkaranCompanyService> logger) : IRahkaranCompanyService
{
    private readonly AppOption _appOptions = options.Value;
    private readonly RahkaranUrlInfo _rahkaranOptions = rahkaranOptions.Value;

    public RahkaranCompanyConfig GetCompanyConfig()
    {
        var result = rahkaranErpCompanyOptionService.GetCompanyFlatConfig(companyContext.CompanyId)
            ?? new RahkaranCompanyConfig();

        result.BaseUrl ??= _rahkaranOptions.BaseUrl;
        result.Username ??= _rahkaranOptions.Username;
        result.Password ??= _rahkaranOptions.Password;

        if (result.BaseUrl != null && result.BaseUrl.EndsWith('/')) result.BaseUrl = result.BaseUrl[..^1];

        return result;
    }

    public IOptions<ConnectionStringOption> GetConnectionStringOption()
    {
        var result = rahkaranErpCompanyOptionService.GetCompanyFlatConfig(companyContext.CompanyId)
            ?? new RahkaranCompanyConfig();

        return new DbOption
        {
            Value = new ConnectionStringOption
            {
                ConnectionString = result.ConnectionString
            }
        };
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

    public bool TryGetCompatibleVersion(out RahkaranVersion compatibleVersion, out string currentVersion)
    {
        currentVersion = GetCurrentVersion();
        try
        {
            compatibleVersion = GetCompatibleVersion();
            return true;
        }
        catch
        {
            compatibleVersion = RahkaranVersion.None;
            return false;
        }
    }

    private RahkaranVersion GetCompatibleVersion()
    {
        try
        {
            var version = GetCurrentVersion();
            logger.LogDebug($"Instantiating SampleErp repository version {version} for company {companyContext.CompanyId} ...");

            version = "V" + version.Replace('.', '_');
            var releaseVersion = version[..version.LastIndexOf('_')];
            var majorVersion = version[..version.IndexOf('_')];

            RahkaranVersion sampleErpVersion = RahkaranVersion.None;
            RahkaranVersion sampleErpLastVersion = RahkaranVersion.V2_0_0;

            if (version.IsBiggerVersion(sampleErpLastVersion.ToString()))
            {
                if (_appOptions.UseLatestVersion)
                {
                    logger.LogDebug("Attempting to use latest repository version.");
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

            if (sampleErpVersion != RahkaranVersion.None)
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
            logger.LogError(ex, "Error in IsCurrentVersionCompatible");
            throw;
        }
    }

    private class DbOption : IOptions<ConnectionStringOption>
    {
        public ConnectionStringOption Value { get; init; } = null!;
    }
}
