using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

public class AuthenticationHttpService(IRahkaranCompanyService company, ILogger<AuthenticationHttpService> logger) :
    AuthenticationBaseService(company, logger)
{
    public override async Task<IToken> Login()
    {
        var client = GetWebService();
        var session = await client.Session();

        var m = HexStringToBytes(session.Rsa.M);
        var e = HexStringToBytes(session.Rsa.E);

        var rsa = new RSACryptoServiceProvider(1024);
        rsa.ImportParameters(new RSAParameters { Exponent = e, Modulus = m });

        var sessionPlusPassword = session.Id + "**" + Password;

        var data = new
        {
            sessionId = session.Id,
            username = Username,
            password = BytesToHexString(rsa.Encrypt(Encoding.Default.GetBytes(sessionPlusPassword), false)),
        };

        using var loginResponse = await client.Login(data);
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

        return TokenService.MakeToken(session.Id, string.Join(';', cookie.Values));
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

        var result = new byte[hex.Length / 2];
        for (var i = 0; i < hex.Length / 2; i++)
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

    public class AuthenticationSession
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
