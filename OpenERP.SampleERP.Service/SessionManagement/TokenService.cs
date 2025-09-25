using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

public class TokenService(IAuthenticationService authService, ILogger<TokenService> logger) : ITokenService
{
    private readonly TimeSpan _idleTimeout = TimeSpan.FromSeconds(60);
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

        logger.LogInformation("Make Token get ready: {sessionId}", _currentToken.SessionId);

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
            if (_calls.Remove(session)) return LogoutAsync(session.Token);
            if (_calls.Count <= 0) return Task.CompletedTask;

            _logoutCts = new CancellationTokenSource();
            return ScheduleLogoutAsync(_logoutCts.Token);
        }
    }

    public Task ReleaseToken()
    {
        IToken token;

        lock (_lock)
        {
            _calls.Clear();

            if (_currentToken is Token tokenToRelease)
            {
                tokenToRelease.IsExpired = true;
            }

            token = _currentToken;
            _currentToken = null;
        }

        return token == null ? Task.CompletedTask : LogoutAsync(token);
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
            await Task.Delay(_idleTimeout, cancelToken);

            IToken token;

            lock (_lock)
            {
                if (_calls.Count <= 0)
                {
                    token = _currentToken;
                    _currentToken = null;
                }
                else token = null;
            }

            if (token != null)
                await LogoutAsync(_currentToken);
        }
        catch (TaskCanceledException)
        {
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during logout for session {sessionId}", _currentToken?.SessionId);
        }
    }

    private async Task LogoutAsync(IToken token)
    {
        if (token == null) return;

        await authService.Logout(token);
        logger.LogInformation("Logged out. SessionId: {sessionId}", token.SessionId);
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