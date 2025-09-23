using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.LoginServices;

internal class RahkaranSessionService(IRahkaranAuthenticationService authService, ILogger<RahkaranSessionService> logger) : 
    IRahkaranSessionService
{
    private readonly TimeSpan _idleTimeout = TimeSpan.FromSeconds(60);
    private readonly IRahkaranAuthenticationService _authService = authService;
    private readonly object _lock = new();
    private readonly HashSet<Session> _calls = [];
    private string _currentSessionId;
    private CancellationTokenSource _logoutCts;

    public IDisposable GetSession()
    {
        Session session;

        lock (_lock)
        {
            if (_currentSessionId == null)
            {
                _currentSessionId = _authService.Login().GetAwaiter().GetResult();
                logger.LogInformation("Logged in. SessionId: {sessionId}", _currentSessionId);
            }

            _logoutCts?.Cancel();
            _logoutCts = null;

            _calls.Add(session = new Session(_currentSessionId, this));
        }

        return session;
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

            await _authService.Logout(sessionId);
            logger.LogInformation("Logged out. SessionId: {sessionId}", sessionId);
        }
        catch (TaskCanceledException) { }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during logout for session {sessionId}", sessionId);
        }
    }

    private class Session(string id, RahkaranSessionService owner) : IDisposable
    {
        public string Id { get; } = id;

        public void Dispose()
        {
            owner.ReleaseSession(this);
        }
    }
}