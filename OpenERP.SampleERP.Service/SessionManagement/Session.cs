using System;
using System.Threading.Tasks;
using Refit;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

public class Session : ISession
{
    private readonly ITokenService _service;

    public Session(ITokenService service)
    {
        _service = service;
        service.MakeTokenGetReady();
    }
    
    public IToken Token { get; private set; }

    public IToken GetToken()
    {
        return Token ??= _service.GetToken(this);
    }

    public async Task<TResult> TryCall<TResult>(Func<IToken, Task<TResult>> action)
    {
        if (Token == null || Token.IsExpired) 
            Token = _service.GetToken(this);

        var unauthorized = false;
        while (true)
        {
            try
            {
                return await action(Token);
            }
            catch (ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                if (unauthorized)
                {
                    throw;
                }
                
                unauthorized =  true;
                _ = _service.ReleaseToken();
                Token = _service.GetToken(this);
            }
        }
    }

    public void Dispose()
    {
        _ = _service.ReleaseSession(this);
        GC.SuppressFinalize(this);
    }
}