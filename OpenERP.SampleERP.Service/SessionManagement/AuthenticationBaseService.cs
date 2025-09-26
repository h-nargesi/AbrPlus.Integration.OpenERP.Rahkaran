using AbrPlus.Integration.OpenERP.SampleERP.Options;
using AbrPlus.Integration.OpenERP.SampleERP.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

internal abstract class AuthenticationBaseService : IAuthenticationService
{
    protected readonly ILogger<AuthenticationBaseService> Logger;
    protected readonly string BaseUrl;
    protected readonly string BasePath;
    protected readonly string Username;
    protected readonly string Password;

    protected AuthenticationBaseService(IOptions<RahkaranUrlInfo> options, ILogger<AuthenticationBaseService> logger)
    {
        Logger = logger;
        
        BaseUrl = options.Value.BaseUrl;
        if (BaseUrl.EndsWith('/')) BaseUrl = BaseUrl[..^1];

        BasePath = options.Value.BasePath.Authentication;
        if (BasePath.StartsWith('/')) BasePath = BasePath[1..];
        if (BasePath.EndsWith('/')) BasePath = BasePath[..^1];

        Username = options.Value.Username;
        Password = options.Value.Password;
    }

    public abstract Task<IToken> Login();

    public async Task Logout(string sessionId)
    {
        var data = new
        {
            sessionId,
        };

        var content = new StringContent(
            data.SerializeJson(),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        using var client = new HttpClient();
        using var sessionResponse = await client.PostAsync($"{BaseUrl}/{BasePath}/logout", content);
        sessionResponse.EnsureSuccessStatusCode();
    }
}
