using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

internal class RahkaranCurrentUserService(IServiceScopeFactory services, ILogger<RahkaranCurrentUserService> logger) : 
    IRahkaranCurrentUserService
{
    private readonly TimeSpan _idleTimeout = TimeSpan.FromSeconds(5);
    private readonly IServiceScopeFactory _services = services;
    private readonly object _lock = new();
    private readonly HashSet<Session> _calls = [];
    private string _currentSessionId;
    private CancellationTokenSource _logoutCts;

    public IDisposable GetSessionId()
    {
        Session session;

        lock (_lock)
        {
            if (_currentSessionId == null)
            {
                _currentSessionId = DoLoginAsync().GetAwaiter().GetResult();
                logger.LogInformation("Logged in. SessionId: {sessionId}", _currentSessionId);
            }

            _logoutCts?.Cancel();
            _logoutCts = null;

            _calls.Add(session = new Session(_currentSessionId, this));
        }

        return session;
    }
    
    private Task<string> DoLoginAsync()
    {
        using var scope = _services.CreateScope();
        var auth = scope.ServiceProvider.GetRequiredService<IRahkaranAuthenticationService>();
        return auth.Login();
    }

    private Task DoLogoutAsync(string sessionId)
    {
        using var scope = _services.CreateScope();
        var auth = scope.ServiceProvider.GetRequiredService<IRahkaranAuthenticationService>();
        return auth.Logout(sessionId);
    }

    private void ReleaseSession(Session session)
    {
        lock (_lock)
        {
            _calls.Remove(session);

            if (_calls.Count <= 0)
            {
                _logoutCts = new CancellationTokenSource();
                _ = ScheduleLogoutAsync(_currentSessionId, _logoutCts.Token);
            }
        }
    }

    private async Task ScheduleLogoutAsync(string sessionId, CancellationToken token)
    {
        try
        {
            await Task.Delay(_idleTimeout, token);

            lock (_lock)
            {
                if (_calls.Count <= 0)
                {
                    _currentSessionId = null;
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

    private class Session(string id, RahkaranCurrentUserService owner) : IDisposable
    {
        public string Id { get; } = id;

        public void Dispose()
        {
            owner.ReleaseSession(this);
        }
    }
}