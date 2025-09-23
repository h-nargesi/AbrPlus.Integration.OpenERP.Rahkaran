using AbrPlus.Integration.OpenERP.SampleERP.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.LoginServices;

internal abstract class RahkaranAuthenticationBaseService(IOptions<RahkaranUrlInfo> options, ILogger<RahkaranAuthenticationBaseService> logger) : 
    IRahkaranAuthenticationService
{
    protected readonly ILogger<RahkaranAuthenticationBaseService> Logger = logger;
    protected readonly RahkaranUrlInfo Options = options.Value;

    public abstract Task<string> Login();

    public async Task Logout(string sessionId)
    {
        var baseUrl = Options.BaseUrl;
        if (baseUrl.EndsWith('/')) baseUrl = baseUrl[..^1];

        var basePath = Options.BasePath.Authentication;
        if (basePath.StartsWith('/')) basePath = basePath[1..];
        if (basePath.EndsWith('/')) basePath = basePath[..^1];

        using var client = new HttpClient();
        using var sessionResponse = await client.GetAsync($"{baseUrl}/{basePath}/logout?sessionId={sessionId}");
        sessionResponse.EnsureSuccessStatusCode();
    }
}
