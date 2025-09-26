using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Collections.Generic;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Customer;

public class CustomerService(ISession session, ICustomerRepository repository, ISampleErpCompanyService company, ILogger<CustomerService> logger) : ICustomerService
{
    private const string BasePath = "/Services/General/PartyManagement/PartyService.svc";
    private readonly RahkaranErpCompanyConfig config = company.GetCompanyConfig();

    public IdentityBundle GetBundle(string key)
    {
        try
        {
            var service = GetRestService();

            var lookup = new
            {
                firstName = key,
                lastName = key,
                company = key,
            };
            var dto = session.TryCall((token) => service.FetchParty(lookup, token.Cookie)).Result;

            return dto.ToBundle();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in CustomerService.GetBundle");
            throw;
        }
    }

    public IdentityBundle GetBundleByCode(string key)
    {
        throw new NotSupportedException("Identity lookup via code is not supported in Rahkaran.");
    }

    public ChangeInfo GetChanges(string lastTrackedVersionStamp)
    {
        long currentTrackingVersion = repository.GetCurrentTrackingVersion();

        if (long.TryParse(lastTrackedVersionStamp, out var lastTrackedVersion) &&
            currentTrackingVersion == lastTrackedVersion)
        {
            return new ChangeInfo() { LastTrackedVersion = lastTrackedVersionStamp };
        }

        var toReturn = new ChangeInfo() { LastTrackedVersion = currentTrackingVersion.ToString() };

        //ToDo : add changes to toReturn.Changes

        throw new NotImplementedException();

        //return toReturn;
    }

    public bool Save(IdentityBundle bundle)
    {
        try
        {
            var service = GetRestService();

            var dto = bundle.ToDto();
            var result = dto.ID > 0 ?
                session.TryCall((token) => service.GenerateParty(dto, token.Cookie)).Result :
                session.TryCall((token) => service.EditParty(dto, token.Cookie)).Result;

            return result?.ValidationErrors.Count == 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in CustomerService.Save");
            throw;
        }
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
        throw new NotImplementedException();
    }

    public decimal GetCustomerBalance(string customerCode)
    {
        throw new NotImplementedException();
    }

    public List<IdentityBalance> GetAllIdentityBalance(IdentityBalanceParams identityBalanceParams)
    {
        var config = company.GetCompanyConfig();
        throw new NotImplementedException();
    }

    private IPartyWebService GetRestService()
    {
        return RestService.For<IPartyWebService>(config.BaseUrl + BasePath);
    }
}
