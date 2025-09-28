using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Service.Customer;
using Refit;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class CustomerServiceTest : BaseServiceTest
{
    [Fact]
    public void GetBundle_Test()
    {
        using var session = GetSession(out _);
        var service = new CustomerService(session, null, Utility.GetLogger<CustomerService>());

        var result = service.GetBundle("1");

        Assert.NotNull(result);
        Assert.Equal("1", result.Id);
        Assert.Equal("کاربر ارشد", result.FirstName);

        result = service.GetBundle("0");

        Assert.NotNull(result);
        Assert.Equal("0", result.Id);
        Assert.Equal("سیستم", result.FirstName);
    }

    [Fact]
    public async Task Save_Test()
    {
        using var session = GetSession(out _);
        var service = new CustomerService(session, null, Utility.GetLogger<CustomerService>());

        var identity = new IdentityBundle
        {
            FirstName = "f1",
            LastName = "l1",
            Gender = "M",
        };

        var saveResult = service.Save(identity);

        Assert.True(saveResult);

        var webService = RestService.For<IPartyWebService>(Company.Object.GetCompanyConfig().BaseUrl + CustomerService.BasePath);
        var data = new { firstName = identity.FirstName, lastName = identity.LastName };
        var savedDto = (await session.TryCall((token) => webService.FetchParty(data, token.Cookie)))?.FetchPartyResult;

        Assert.NotNull(savedDto);
        Assert.Equal(identity.FirstName, savedDto.FirstName);
        Assert.Equal(identity.LastName, savedDto.LastName);

        var saved = service.GetBundle(savedDto.ID.ToString());

        Assert.NotNull(saved);
        Assert.Equal(identity.FirstName, saved.FirstName);
        Assert.Equal(identity.LastName, saved.LastName);

        saved.FirstName += "2";
        saved.LastName += "2";
        identity = saved;

        saveResult = service.Save(saved);

        Assert.True(saveResult);

        saved = service.GetBundle(savedDto.ID.ToString());

        Assert.NotNull(saved);
        Assert.Equal(identity.FirstName, saved.FirstName);
        Assert.Equal(identity.LastName, saved.LastName);
    }
}
