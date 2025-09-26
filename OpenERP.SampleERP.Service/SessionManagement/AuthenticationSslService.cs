using AbrPlus.Integration.OpenERP.SampleERP.Options;
using AbrPlus.Integration.OpenERP.SampleERP.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

internal class AuthenticationSslService(IOptions<RahkaranUrlInfo> options, ILogger<AuthenticationSslService> logger, ISampleErpCompanyService company) : 
    AuthenticationBaseService(options, logger, company)
{
    public override async Task<IToken> Login()
    {
        var data = new
        {
            username = Username,
            password = Password,
        };

        var content = new StringContent(
            data.SerializeJson(),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        using var client = new HttpClient();
        using var response = await client.PostAsync($"{BaseUrl}/{BasePath}/ssllogin", content);
        response.EnsureSuccessStatusCode();

        if (!response.Headers.TryGetValues("Set-Cookie", out var textCookie))
        {
            textCookie = [];
        }
        
        var sessionCookie = string.Join(",", textCookie);

        if (string.IsNullOrEmpty(sessionCookie))
            throw new Exception("The 'Set-Cookie' not found in login response.");

        var cookie = sessionCookie.Split(',', ';')
            .GroupBy(c => c.Trim().Split('=')[0], StringComparer.OrdinalIgnoreCase)
            .ToDictionary(k => k.Key, v => v.First());

        return TokenService.MakeToken(null, string.Join(';', cookie.Values));
    }
}
