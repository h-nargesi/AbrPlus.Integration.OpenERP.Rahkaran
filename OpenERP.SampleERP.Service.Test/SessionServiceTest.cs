using AbrPlus.Integration.OpenERP.SampleERP.Service;
using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using AbrPlus.Integration.OpenERP.SampleERP.Settings;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class SessionServiceTest
{
    private readonly Mock<IAuthenticationService> AuthenticationService = new();
    private readonly Mock<IRahkaranCompanyService> Company = new();

    public SessionServiceTest()
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
    public async Task MakeTokenGetReady_Test()
    {
        var service = new TokenService(AuthenticationService.Object, Utility.GetLogger<TokenService>(), Company.Object);

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
        var service = new TokenService(AuthenticationService.Object, Utility.GetLogger<TokenService>(), Company.Object);

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
        var service = new TokenService(AuthenticationService.Object, Utility.GetLogger<TokenService>(), Company.Object);

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

        using var newSession = new Session(service, Company.Object);
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
        var service = new TokenService(AuthenticationService.Object, Utility.GetLogger<TokenService>(), Company.Object);

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

        using var newSession = new Session(service, Company.Object);
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

    private void GenerateToken(TokenService service, int count, List<IToken> tokens)
    {
        var sessions = new List<ISession>(count);

        for (var i = 0; i < count; i++)
        {
            var session = new Session(service, Company.Object);
            sessions.Add(session);
            tokens.Add(session.GetToken());
        }

        foreach (var session in sessions)
        {
            session.Dispose();
        }
    }
}