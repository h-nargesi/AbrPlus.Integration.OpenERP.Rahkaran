using AbrPlus.Integration.OpenERP.SampleERP.Service.Options;
using AbrPlus.Integration.OpenERP.SampleERP.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

internal class RahkaranAuthenticationSslService(IOptions<RahkaranUrlOption> options, ILogger<RahkaranAuthenticationSslService> logger) : 
    RahkaranAuthenticationBaseService(options, logger)
{
    public override async Task<string> Login()
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

        using var response = await Client.PostAsync($"{AuthenticationServiceAddress}/ssllogin", content);
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
