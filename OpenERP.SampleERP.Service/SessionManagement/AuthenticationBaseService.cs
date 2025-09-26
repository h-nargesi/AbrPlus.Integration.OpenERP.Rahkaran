using Microsoft.Extensions.Logging;
using Refit;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

public abstract class AuthenticationBaseService : IAuthenticationService
{
    protected const string BasePath = "/Services/Framework/AuthenticationService.svc";

    protected readonly ILogger<AuthenticationBaseService> Logger;
    protected readonly string BaseUrl;
    protected readonly string Username;
    protected readonly string Password;

    protected AuthenticationBaseService(ISampleErpCompanyService company, ILogger<AuthenticationBaseService> logger)
    {
        Logger = logger;

        var config = company.GetCompanyConfig();

        BaseUrl = config?.BaseUrl;
        Username = config?.Username;
        Password = config?.Password;
    }

    public abstract Task<IToken> Login();

    public async Task Logout(string sessionId)
    {
        var client = GetWebService();
        var sessionResponse = await client.Logout(sessionId);
        sessionResponse.EnsureSuccessStatusCode();
    }

    protected IAuthenticationWebService GetWebService()
    {
        return RestService.For<IAuthenticationWebService>(BaseUrl + BasePath);
    }
}
