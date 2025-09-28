using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

public class AuthenticationSslService(IRahkaranCompanyService company, ILogger<AuthenticationSslService> logger) : 
    AuthenticationBaseService(company, logger)
{
    public override async Task<IToken> Login()
    {
        var data = new
        {
            username = Username,
            password = Password,
        };

        var client = GetWebService();
        using var response = await client.LoginSsl(data);
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
