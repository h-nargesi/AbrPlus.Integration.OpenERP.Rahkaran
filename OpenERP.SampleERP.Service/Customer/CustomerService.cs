using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.Enums;
using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using AbrPlus.Integration.OpenERP.SampleERP.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Customer;

public class CustomerService(ISession session, IPartyRepository repository, ILogger<CustomerService> logger)
    : ServiceBase(repository), ICustomerService
{
    public async Task<IdentityBundle> GetBundle(string key)
    {
        try
        {
            if (!long.TryParse(key, out var partRef))
            {
                // TODO use custom exception
                throw new Exception($"Invalid Identity Key: {key}");
            }

            var service = session.GetWebService<IPartyWebService>(IPartyWebService.BasePath);

            var dto = await session.TryCall((token) => service.PartyByRef(new { partRef }, token.Cookie));

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

    public decimal GetCustomerBalance(string customerCode)
    {
        throw new NotSupportedException("Identity lookup via code is not supported in Rahkaran.");
    }

    public List<IdentityBalance> GetAllIdentityBalance(IdentityBalanceParams identityBalanceParams)
    {
        throw new NotSupportedException("Identity lookup via code is not supported in Rahkaran.");
    }

    public bool Validate(IdentityBundle item)
    {
        throw new NotSupportedException("Identity lookup via code is not supported in Rahkaran.");
    }

    public async Task<bool> Save(IdentityBundle bundle)
    {
        try
        {
            var service = session.GetWebService<IPartyWebService>(IPartyWebService.BasePath);

            var dto = bundle.ToDto();

            var result = dto.ID > 0 ?
                await session.TryCall((token) => service.EditParty([dto], token.Cookie)) :
                await session.TryCall((token) => service.GenerateParty([dto], token.Cookie));

            var messages = result?.FirstOrDefault()?.ValidationErrors;

            return messages?.Length > 0 ? throw
                // TODO use custom exception
                new Exception(string.Join('\n', messages)) : true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in CustomerService.Save");
            throw;
        }
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
}
