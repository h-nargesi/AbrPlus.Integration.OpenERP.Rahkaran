using AbrPlus.Integration.OpenERP.SampleERP.Options;
using AbrPlus.Integration.OpenERP.SampleERP.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.LoginServices;

internal class RahkaranAuthenticationSslService(IOptions<RahkaranUrlInfo> options, ILogger<RahkaranAuthenticationSslService> logger) : 
    RahkaranAuthenticationBaseService(options, logger)
{
    public override async Task<string> Login()
    {
        var baseUrl = Options.BaseUrl;
        if (baseUrl.EndsWith('/')) baseUrl = baseUrl[..^1];

        var basePath = Options.BasePath.Authentication;
        if (basePath.StartsWith('/')) basePath = basePath[1..];
        if (basePath.EndsWith('/')) basePath = basePath[..^1];

        var data = new
        {
            username = Options.Username,
            password = Options.Password,
        };

        var content = new StringContent(
            data.SerializeJson(),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        using var client = new HttpClient();
        using var response = await client.PostAsync($"{baseUrl}/{basePath}/ssllogin", content);
        response.EnsureSuccessStatusCode();

        if (!response.Headers.TryGetValues("Set-Cookie", out var textCookie))
        {
            textCookie = [];
        }
        
        var sessionCookie = string.Join(",", textCookie);
        
        return string.IsNullOrEmpty(sessionCookie) ? 
            throw new Exception("The 'Set-Cookie' not found in login response.") : sessionCookie;
    }
}
