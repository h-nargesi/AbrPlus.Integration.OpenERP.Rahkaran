using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using AbrPlus.Integration.OpenERP.SampleERP.Shared;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Customer;

public class CustomerService(ISession session, ICustomerRepository repository, ISampleErpCompanyService company, ILogger<CustomerService> logger) : ICustomerService
{
    public const string BasePath = "/General/PartyManagement/Services/PartyService.svc";
    private readonly RahkaranErpCompanyConfig config = company.GetCompanyConfig();

    public IdentityBundle GetBundle(string key)
    {
        try
        {
            if (!long.TryParse(key, out var partRef))
            {
                // TODO use custom exception
                throw new Exception($"Invlid Identity Key: {key}");
            }

            var service = GetRestService();

            var dto = session.TryCall((token) => service.PartyByRef(new { partRef }, token.Cookie)).Result;

            return dto.GetPartyResult.ToBundle();
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
                session.TryCall((token) => service.EditParty([dto], token.Cookie)).Result :
                session.TryCall((token) => service.GenerateParty([dto], token.Cookie)).Result;

            var messages = result?.FirstOrDefault()?.ValidationErrors;

            if (messages?.Length > 0)
            {
                // TODO use custom exception
                throw new Exception(string.Join('\n', messages));
            }

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in CustomerService.Save");
            throw;
        }
    }

    public bool Validate(IdentityBundle item)
    {
        throw new NotSupportedException("Identity lookup via code is not supported in Rahkaran.");
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
        throw new NotSupportedException("Identity lookup via code is not supported in Rahkaran.");
    }

    public List<IdentityBalance> GetAllIdentityBalance(IdentityBalanceParams identityBalanceParams)
    {
        throw new NotSupportedException("Identity lookup via code is not supported in Rahkaran.");
    }

    private IPartyWebService GetRestService()
    {
        return RestService.For<IPartyWebService>(config.BaseUrl + BasePath, JsonObjectExtension.RefitSettings);
    }
}
