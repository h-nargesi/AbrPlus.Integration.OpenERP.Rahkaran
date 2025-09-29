using AbrPlus.Integration.OpenERP.SampleERP.Service;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using FluentAssertions;
using Refit;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class SessionTest
{
    private readonly Mock<IAuthenticationService> AuthenticationService = new();
    private readonly Mock<IRahkaranCompanyService> Company = new();

    public SessionTest()
    {
        Company.Setup(x => x.GetCompanyConfig())
            .Returns(new RahkaranCompanyConfig
            {
                IdleTimeout = 10,
            });

        int Counter = 0;
        AuthenticationService.Setup(x => x.Login())
            .Returns(() => Task.Run(() =>
            {
                Thread.Sleep(1000);
                var ticket = ++Counter;
                return TokenService.MakeToken($"S{ticket}", $"C{ticket}");
            }));
    }

    [Fact]
    public void GetToken_Simple_Exception()
    {
        var service = new TokenService(AuthenticationService.Object, Utility.GetLogger<TokenService>(), Company.Object);
        using var session = new Session(service, Company.Object);

        var action = () => TryCallWithSimpleException(session);

        action.Should().Throw<Exception>();
    }

    [Fact]
    public void GetToken_Unauthorized_Always_Test()
    {
        var service = new TokenService(AuthenticationService.Object, Utility.GetLogger<TokenService>(), Company.Object);
        using var session = new Session(service, Company.Object);

        Action action = () => TryCallWithAlwaysUnauthorizedException(session);

        action.Should().Throw<ApiException>();
    }

    [Fact]
    public void GetToken_Unauthorized_Test()
    {
        var service = new TokenService(AuthenticationService.Object, Utility.GetLogger<TokenService>(), Company.Object);
        using var session = new Session(service, Company.Object);

        var token1 = session.GetToken();

        Action action = () => TryCallWithOnceUnauthorizedException(session);

        action.Should().NotThrow<ApiException>();

        var token2 = session.GetToken();

        Assert.NotNull(token1);
        Assert.NotNull(token1.SessionId);
        Assert.NotNull(token1.Cookie);

        Assert.NotEqual(token2, token1);
        Assert.NotEqual(token2.SessionId, token1?.SessionId);
        Assert.NotEqual(token2.Cookie, token1?.Cookie);
    }

    private static void TryCallWithSimpleException(Session session)
    {
        session.TryCall<bool>((token) => throw new Exception("Test Exception")).Wait();
    }

    private static void TryCallWithAlwaysUnauthorizedException(Session session)
    {
        session.TryCall<bool>((token) => throw Throw401()).Wait();
    }

    private static void TryCallWithOnceUnauthorizedException(Session session)
    {
        var expired = false;
        session.TryCall((token) =>
        {
            if (expired)
            {
                return Task.FromResult(true);
            }

            expired = true;
            throw Throw401();
        }).Wait();
    }

    private static ApiException Throw401()
    {
        var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
        {
            Content = new StringContent("Unauthorized")
        };

        return ApiException.Create(
            new HttpRequestMessage(HttpMethod.Post, Utility.RahkaranBaseUrl),
            HttpMethod.Post,
            response,
            new RefitSettings()).Result;
    }
}