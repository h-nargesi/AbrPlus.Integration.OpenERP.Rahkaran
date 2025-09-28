using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

public class TokenService(IAuthenticationService authService, ILogger<TokenService> logger, IRahkaranCompanyService company) : ITokenService
{
    private readonly object _lock = new();
    private readonly HashSet<ISession> _calls = [];
    private Task<IToken> _intentToGenerateToken;
    private IToken _currentToken;
    private CancellationTokenSource _logoutCts;

    public Task MakeTokenGetReady()
    {
        Task result;

        lock (_lock)
        {
            result = GetTokenOrTask();
        }

        logger.LogInformation("Make Token get ready ...");

        return result;
    }

    public IToken GetToken(ISession session)
    {
        lock (_lock)
        {
            _logoutCts?.Cancel();
            _logoutCts = null;

            if (_currentToken == null)
            {
                _currentToken = GetTokenOrTask().GetAwaiter().GetResult();
                logger.LogInformation("Logged in. SessionId: {sessionId}", _currentToken.SessionId);
            }

            _calls.Add(session);
        }

        return _currentToken;
    }

    public Task ReleaseSession(ISession session)
    {
        lock (_lock)
        {
            if (!_calls.Remove(session)) return LogoutAsync(session.Token?.SessionId);
            if (session.Token == null || _calls.Count > 0) return Task.CompletedTask;

            _logoutCts = new CancellationTokenSource();
            return ScheduleLogoutAsync(_logoutCts.Token);
        }
    }

    public Task ReleaseToken()
    {
        string sessionId;

        lock (_lock)
        {
            _calls.Clear();

            if (_currentToken is Token tokenToRelease)
            {
                tokenToRelease.IsExpired = true;
            }

            sessionId = _currentToken?.SessionId;
            _currentToken = null;
            _intentToGenerateToken = null;
        }

        return sessionId == null ? Task.CompletedTask : LogoutAsync(sessionId);
    }

    private Task<IToken> GetTokenOrTask()
    {
        Task<IToken> result;

        if (_currentToken != null) result = Task.FromResult(_currentToken);
        else if (_intentToGenerateToken != null) result = _intentToGenerateToken;
        else result = _intentToGenerateToken = authService.Login();

        return result;
    }

    private async Task ScheduleLogoutAsync(CancellationToken cancelToken)
    {
        try
        {
            var config = company.GetCompanyConfig();
            var IdleTimeout = TimeSpan.FromSeconds(config.IdleTimeout < 10 ? 10 : config.IdleTimeout);

            await Task.Delay(IdleTimeout, cancelToken);

            string sessionId;

            lock (_lock)
            {
                if (_calls.Count <= 0)
                {
                    sessionId = _currentToken?.SessionId;
                    _currentToken = null;
                    _intentToGenerateToken = null;
                }
                else sessionId = null;
            }

            if (sessionId != null)
                await LogoutAsync(sessionId);
        }
        catch (TaskCanceledException)
        {
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during logout for session {sessionId}", _currentToken?.SessionId);
        }
    }

    private async Task LogoutAsync(string sessionId)
    {
        if (sessionId == null) return;

        await authService.Logout(sessionId);
        logger.LogInformation("Logged out. SessionId: {sessionId}", sessionId);
    }

    public static IToken MakeToken(string sessionId, string cookie)
    {
        return new Token(sessionId, cookie);
    }

    private class Token(string sessionId, string cookie) : IToken
    {
        public string SessionId { get; } = sessionId;

        public string Cookie { get; } = cookie;

        public bool IsExpired { get; set; }
    }
}