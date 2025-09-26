using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Options;
using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public class CustomerService(ISession session, IOptions<RahkaranUrlInfo> options, ILogger<CustomerService> logger, ISampleErpCompanyService company, ICustomerRepository repository) : ICustomerService
{
    public IdentityBundle GetBundle(string key)
    {
        try
        {
            var setting = company.GetCompanyConfig();

            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetBundle");
            throw;
        }
    }

    public IdentityBundle GetBundleByCode(string key)
    {
        try
        {
            var setting = company.GetCompanyConfig();

            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in GetBundleByCode");
            throw;
        }
    }

    public ChangeInfo GetChanges(string lastTrackedVersionStamp)
    {
        var config = company.GetCompanyConfig();

        long currentTrackingVersion = repository.GetCurrentTrackingVersion();

        if (long.TryParse(lastTrackedVersionStamp, out var lastTrackedVersion) &&
            currentTrackingVersion == lastTrackedVersion)
        {
            return new ChangeInfo() { LastTrackedVersion = lastTrackedVersionStamp };
        }

        var toReturn = new ChangeInfo() { LastTrackedVersion = currentTrackingVersion.ToString() };

        //ToDo : add changes to toReturn.Changes


        return toReturn;
    }

    public bool Save(IdentityBundle item)
    {
        throw new NotImplementedException();
    }

    public bool Validate(IdentityBundle item)
    {
        throw new NotImplementedException();
    }

    public void SetTrackingStatus(bool enabled)
    {
        if (enabled)
        {
            repository.EnableTableTracking();
        }
        else
        {
            repository.DisableTableTracking();
        }
    }

    public string[] GetAllIds()
    {
        var config = company.GetCompanyConfig();

        throw new NotImplementedException();
    }

    public decimal GetCustomerBalance(string customerCode)
    {
        var config = company.GetCompanyConfig();
        throw new NotImplementedException();
    }

    public List<IdentityBalance> GetAllIdentityBalance(IdentityBalanceParams identityBalanceParams)
    {
        var config = company.GetCompanyConfig();
        throw new NotImplementedException();
    }
}
