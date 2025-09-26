using AbrPlus.Integration.OpenERP.SampleERP.Service;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using Microsoft.Extensions.Logging;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class SessionServiceTest
{
    private readonly Mock<ILogger<TokenService>> Logger = new();
    private readonly Mock<IAuthenticationService> AuthenticationService = new();
    private readonly Mock<ISampleErpCompanyService> Company = new();

    public SessionServiceTest()
    {
        Company.Setup(x => x.GetCompanyConfig())
            .Returns(new RahkaranErpCompanyConfig
            {
                IdleTimeout = 10,
            });

        Logger.Setup(x => x.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => true),
            null,
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ));

        Logger.Setup(x => x.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => true),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ));

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
    public async Task MakeTokenGetReady_Test()
    {
        var service = new TokenService(AuthenticationService.Object, Logger.Object, Company.Object);

        var task = service.MakeTokenGetReady();

        Assert.NotNull(task);

        var token = await Assert.IsAssignableFrom<Task<IToken>>(task);

        Assert.NotNull(token);
        Assert.NotNull(token.SessionId);
        Assert.NotNull(token.Cookie);
    }

    [Fact]
    public void GetToken_Test()
    {
        var service = new TokenService(AuthenticationService.Object, Logger.Object, Company.Object);

        var count = 5;
        var loop = 2;
        var tokens = new List<IToken>(count * loop);

        for (var i = 0; i < loop; i++)
            GenerateToken(service, count, tokens);

        IToken? tokenBefore = null;
        foreach (var token in tokens)
        {
            Assert.NotNull(token);
            Assert.NotNull(token.SessionId);
            Assert.NotNull(token.Cookie);

            if (tokenBefore != null)
            {
                Assert.Equal(token, tokenBefore);
            }

            tokenBefore = token;
        }
    }

    [Fact]
    public void GetToken_TimePassed_Test()
    {
        var service = new TokenService(AuthenticationService.Object, Logger.Object, Company.Object);

        var count = 5;
        var loop = 2;
        var tokens = new List<IToken>(count * loop);

        for (var i = 0; i < loop; i++)
            GenerateToken(service, count, tokens);

        IToken? tokenBefore = null;
        foreach (var token in tokens)
        {
            Assert.NotNull(token);
            Assert.NotNull(token.SessionId);
            Assert.NotNull(token.Cookie);

            if (tokenBefore != null)
            {
                Assert.Equal(token, tokenBefore);
            }

            tokenBefore = token;
        }

        Thread.Sleep((1 + Company.Object.GetCompanyConfig().IdleTimeout) * 1000);

        using var newSession = new Session(service);
        var newToken = newSession.GetToken();

        Assert.NotNull(newToken);
        Assert.NotNull(newToken.SessionId);
        Assert.NotNull(newToken.Cookie);

        Assert.NotEqual(newToken, tokenBefore);
        Assert.NotEqual(newToken.SessionId, tokenBefore?.SessionId);
        Assert.NotEqual(newToken.Cookie, tokenBefore?.Cookie);
    }

    [Fact]
    public void GetToken_ReleaseToken_401_Test()
    {
        var service = new TokenService(AuthenticationService.Object, Logger.Object, Company.Object);

        var count = 5;
        var tokens = new List<IToken>(count);
        GenerateToken(service, count, tokens);

        IToken? tokenBefore = null;
        foreach (var token in tokens)
        {
            Assert.NotNull(token);
            Assert.NotNull(token.SessionId);
            Assert.NotNull(token.Cookie);

            if (tokenBefore != null)
            {
                Assert.Equal(token, tokenBefore);
            }

            tokenBefore = token;
        }

        Thread.Sleep((1 + Company.Object.GetCompanyConfig().IdleTimeout) * 1000);

        using var newSession = new Session(service);
        newSession.GetToken();
        service.ReleaseToken();
        var newToken = newSession.GetToken();

        Assert.NotNull(newToken);
        Assert.NotNull(newToken.SessionId);
        Assert.NotNull(newToken.Cookie);

        Assert.NotEqual(newToken, tokenBefore);
        Assert.NotEqual(newToken.SessionId, tokenBefore?.SessionId);
        Assert.NotEqual(newToken.Cookie, tokenBefore?.Cookie);
    }

    private static void GenerateToken(TokenService service, int count, List<IToken> tokens)
    {
        var sessions = new List<ISession>(count);

        for (var i = 0; i < count; i++)
        {
            var session = new Session(service);
            sessions.Add(session);
            tokens.Add(session.GetToken());
        }

        foreach (var session in sessions)
        {
            session.Dispose();
        }
    }
}