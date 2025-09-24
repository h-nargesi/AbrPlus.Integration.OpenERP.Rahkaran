using AbrPlus.Integration.OpenERP.SampleERP.Models;
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
    private SessionInfo _currentSessionInfo;
    private CancellationTokenSource _logoutCts;

    public IDisposable GetSession()
    {
        Session session;

        lock (_lock)
        {
            if (_currentSessionInfo == null)
            {
                _currentSessionInfo = _authService.Login().GetAwaiter().GetResult();
                logger.LogInformation("Logged in. SessionId: {sessionId}", _currentSessionInfo.Id);
            }

            _logoutCts?.Cancel();
            _logoutCts = null;

            _calls.Add(session = new Session(_currentSessionInfo, this));
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
                _ = ScheduleLogoutAsync(_currentSessionInfo.Id, _logoutCts.Token);
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
                    _currentSessionInfo = null;
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

    private class Session(SessionInfo sessionInfo, RahkaranSessionService owner) : IDisposable
    {
        private readonly SessionInfo _sessionInfo = sessionInfo;

        public string Id => _sessionInfo.Id;

        public string Coockie => _sessionInfo.Coockie;

        public void Dispose()
        {
            owner.ReleaseSession(this);
        }
    }
}