using AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;
using Microsoft.Extensions.Logging;
using Moq;

namespace AbrPlus.Integration.OpenERP.SampleERP.Test;

public class SessionServiceTest
{
    private readonly Mock<ILogger<TokenService>> Logger = new();
    private readonly Mock<IAuthenticationService> AuthenticationService = new();
    private int Counter = 0;

    public SessionServiceTest()
    {
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

        AuthenticationService.Setup(x => x.Login())
            .Returns(Task.Run(() => {
                Thread.Sleep(1000);
                int ticket;
                lock (AuthenticationService) ticket = ++Counter;
                return TokenService.MakeToken($"S{ticket}", $"C{ticket}");
            }));
    }

    [Fact]
    public async Task MakeTokenGetReady_Test()
    {
        var service = new TokenService(AuthenticationService.Object, Logger.Object);

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
        var service = new TokenService(AuthenticationService.Object, Logger.Object);

        var session = new Session(service);

        var token1 = service.GetToken(session);
        var token2 = service.GetToken(session);

        Assert.NotNull(token1);
        Assert.NotNull(token1.SessionId);
        Assert.NotNull(token1.Cookie);

        Assert.NotNull(token2);
        Assert.NotNull(token2.SessionId);
        Assert.NotNull(token2.Cookie);

        Assert.Equal(token1, token2);
    }
}