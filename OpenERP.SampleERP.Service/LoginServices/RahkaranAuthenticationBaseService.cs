using AbrPlus.Integration.OpenERP.SampleERP.Service.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.LoginServices;

internal abstract class RahkaranAuthenticationBaseService(IOptions<RahkaranUrlOption> options, ILogger<RahkaranAuthenticationBaseService> logger) : 
    IRahkaranAuthenticationService
{
    protected readonly ILogger<RahkaranAuthenticationBaseService> Logger = logger;

    private const string AuthenticationServiceRelativeAddress = "/Services/Framework/AuthenticationService.svc";
    protected readonly string AuthenticationServiceAddress = options.Value.BaseUrl + AuthenticationServiceRelativeAddress;
    protected readonly string Username = options.Value.Username;
    protected readonly string Password = options.Value.Password;

    public abstract Task<string> Login();

    public async Task Logout(string sessionId)
    {
        using var client = new HttpClient();
        using var sessionResponse = await client.GetAsync($"{AuthenticationServiceAddress}/session?sessionId={sessionId}");
        sessionResponse.EnsureSuccessStatusCode();
    }
}
