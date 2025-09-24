using AbrPlus.Integration.OpenERP.SampleERP.Models;
using AbrPlus.Integration.OpenERP.SampleERP.Options;
using AbrPlus.Integration.OpenERP.SampleERP.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.LoginServices;

internal class RahkaranAuthenticationHttpService(IOptions<RahkaranUrlInfo> options, ILogger<RahkaranAuthenticationHttpService> logger) :
    RahkaranAuthenticationBaseService(options, logger)
{
    public override async Task<SessionInfo> Login()
    {
        var baseUrl = Options.BaseUrl;
        if (baseUrl.EndsWith('/')) baseUrl = baseUrl[..^1];

        var basePath = Options.BasePath.Authentication;
        if (basePath.StartsWith('/')) basePath = basePath[1..];
        if (basePath.EndsWith('/')) basePath = basePath[..^1];

        using var client = new HttpClient();
        using var sessionResponse = await client.GetAsync($"{baseUrl}/{basePath}/session");
        sessionResponse.EnsureSuccessStatusCode();
        var session = JsonConvert.DeserializeObject<AuthenticationSession>(await sessionResponse.Content.ReadAsStringAsync());

        var m = HexStringToBytes(session.Rsa.M);
        var e = HexStringToBytes(session.Rsa.E);

        var rsa = new RSACryptoServiceProvider(1024);
        rsa.ImportParameters(new RSAParameters { Exponent = e, Modulus = m });

        var sessionPlusPassword = session.Id + "**" + Options.Password;

        var data = new
        {
            sessionId = session.Id,
            username = Options.Username,
            password = BytesToHexString(rsa.Encrypt(Encoding.Default.GetBytes(sessionPlusPassword), false)),
        };

        var content = new StringContent(
            data.SerializeJson(),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        using var loginResponse = await client.PostAsync($"{baseUrl}/{basePath}/login", content);
        loginResponse.EnsureSuccessStatusCode();

        if (!loginResponse.Headers.TryGetValues("Set-Cookie", out var textCookie))
        {
            textCookie = [];
        }

        var sessionCookie = string.Join(",", textCookie);

        if (string.IsNullOrEmpty(sessionCookie))
            throw new Exception("The 'Set-Cookie' not found in login response.");

        var cookie = sessionCookie.Split(',', ';')
            .GroupBy(c => c.Trim().Split('=')[0], StringComparer.OrdinalIgnoreCase)
            .ToDictionary(k => k.Key, v => v.First());

        return new SessionInfo
        {
            Id = session.Id,
            Coockie = string.Join(';', cookie.Values),
        };
    }

    private static byte[] HexStringToBytes(string hex)
    {
        if (hex.Length == 0)
        {
            return [0];
        }

        if (hex.Length % 2 == 1)
        {
            hex = "0" + hex;
        }

        byte[] result = new byte[hex.Length / 2];
        for (int i = 0; i < hex.Length / 2; i++)
        {
            result[i] = byte.Parse(hex.Substring(2 * i, 2), NumberStyles.AllowHexSpecifier);
        }

        return result;
    }

    private static string BytesToHexString(byte[] bytes)
    {
        var hexString = new StringBuilder(64);

        foreach (var b in bytes)
        {
            hexString.Append($"{b:X2}");
        }

        return hexString.ToString();
    }

    private class AuthenticationSession
    {
        public string Id { get; set; }

        public RsaPublicParameters Rsa { get; set; }

        public class RsaPublicParameters
        {
            public string M { get; set; }

            public string E { get; set; }
        }
    }
}
