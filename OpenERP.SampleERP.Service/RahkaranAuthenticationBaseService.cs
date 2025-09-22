using AbrPlus.Integration.OpenERP.SampleERP.Service.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

abstract class RahkaranAuthenticationBaseService(IOptions<RahkaranUrlOption> options, ILogger<RahkaranAuthenticationBaseService> logger) : 
    IRahkaranAuthenticationServrice
{
    protected readonly HttpClient client = new();
    protected readonly ILogger<RahkaranAuthenticationBaseService> logger = logger;

    private const string AuthenticationServiceRelativeAddress = "/Services/Framework/AuthenticationService.svc";
    protected readonly string AuthenticationServiceAddress = options.Value.BaseUrl + AuthenticationServiceRelativeAddress;

    public abstract Task<string> Login(string username, string password);

    public async Task Logout(string sessionId)
    {
        using var session_response = await client.GetAsync($"{AuthenticationServiceAddress}/session?sessionId={sessionId}");
        session_response.EnsureSuccessStatusCode();
    }
}
