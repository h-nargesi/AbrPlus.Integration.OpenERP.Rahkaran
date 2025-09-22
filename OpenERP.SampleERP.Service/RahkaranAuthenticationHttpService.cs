using AbrPlus.Integration.OpenERP.SampleERP.Service.Options;
using AbrPlus.Integration.OpenERP.SampleERP.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

internal class RahkaranAuthenticationHttpService(IOptions<RahkaranUrlOption> options, ILogger<RahkaranAuthenticationHttpService> logger) : 
    RahkaranAuthenticationBaseService(options, logger)
{
    public override async Task<string> Login(string username, string password)
    {
        using var session_response = await client.GetAsync($"{AuthenticationServiceAddress}/session");
        session_response.EnsureSuccessStatusCode();

        var session = JsonConvert.DeserializeObject<AuthenticationSession>(await session_response.Content.ReadAsStringAsync());
        session_response.Dispose();

        var m = HexStringToBytes(session.RSA.M);
        var e = HexStringToBytes(session.RSA.E);

        var rsa = new RSACryptoServiceProvider(1024);
        rsa.ImportParameters(new RSAParameters { Exponent = e, Modulus = m });

        var sessionPlusPassword = session.Id + "**" + password;

        var data = new
        {
            sessionId = session.Id,
            username,
            password = BytesToHexString(rsa.Encrypt(Encoding.Default.GetBytes(sessionPlusPassword), false)),
        };

        var content = new StringContent(
            data.SerializeJson(),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

        using var login_response = await client.PostAsync($"{AuthenticationServiceAddress}/login", content);
        login_response.EnsureSuccessStatusCode();

        if (!login_response.Headers.TryGetValues("Set-Cookie", out var textCookie) || textCookie is null)
        {
            throw new Exception("The 'Set-Cookie' not found in login response.");
        }

        return string.Join(",", textCookie);
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

    private static string BytesToHexString(byte[] input)
    {
        var hexString = new StringBuilder(64);

        for (int i = 0; i < input.Length; i++)
        {
            hexString.Append(string.Format("{0:X2}", input[i]));
        }

        return hexString.ToString();
    }

    private class AuthenticationSession
    {
        public string Id { get; set; }

        public RSAPublicParameters RSA { get; set; }

        public partial class RSAPublicParameters
        {
            public string M { get; set; }

            public string E { get; set; }
        }
    }
}
