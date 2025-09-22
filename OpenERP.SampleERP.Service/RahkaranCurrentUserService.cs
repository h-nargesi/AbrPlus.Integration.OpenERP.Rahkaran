namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

internal class RahkaranCurrentUserService(IServiceScopeFactory services, ILogger<RahkaranCurrentUserService> logger) : 
    IRahkaranCurrentUserService
{
    private readonly TimeSpan idleTimeout = TimeSpan.FromSeconds(5);
    private readonly IServiceScopeFactory services = services;
    private readonly object @lock = new();
    private readonly HashSet<Session> calls = new();
    private string currentSessionId;
    private CancellationTokenSource logoutCts;

    public IDisposable GetSessionId()
    {
        Session session;

        lock (@lock)
        {
            if (currentSessionId == null)
            {
                currentSessionId = DoLoginAsync().GetAwaiter().GetResult();
                logger.LogInformation("Logged in. SessionId: {sessionId}", currentSessionId);
            }

            logoutCts?.Cancel();
            logoutCts = null;

            calls.Add(session = new Session(currentSessionId));
        }

        return session;
    }
    
    private Task<string> DoLoginAsync()
    {
        using var scope = services.CreateScope();
        var auth = scope.ServiceProvider.GetRequiredService<IRahkaranAuthenticationService>();
        return auth.Login();
    }

    private Task DoLogoutAsync(string sessionId)
    {
        using var scope = scopeFactory.CreateScope();
        var auth = scope.ServiceProvider.GetRequiredService<IRahkaranAuthenticationService>();
        return auth.Logout(sessionId);
    }

    private void ReleaseSession(Session session)
    {
        lock (@lock)
        {
            calls.Remove(session);

            if (calls.Count <= 0)
            {
                logoutCts = new CancellationTokenSource();
                _ = ScheduleLogoutAsync(currentSessionId, logoutCts.Token);
            }
        }
    }

    private async Task ScheduleLogoutAsync(string sessionId, CancellationToken token)
    {
        try
        {
            await Task.Delay(idleTimeout, token);

            lock (@lock)
            {
                if (calls.Count <= 0)
                {
                    currentSessionId = null;
                }
            }

            await DoLogoutAsync(sessionId);
            logger.LogInformation("Logged out. SessionId: {sessionId}", sessionId);
        }
        catch (TaskCanceledException) { }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during logout for session {sessionId}", sessionId);
        }
    }

    class Session(string id, RahkaranCurrentUserService owener) : IDisposable
    {
        public string Id { get; } = id;

        public void Dispose()
        {
            owener.ReleaseSession(this);
        }
    }
}