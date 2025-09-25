using System;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

internal class Session : ISession
{
    private readonly ITokenService _service;

    protected Session(ITokenService service)
    {
        _service = service;
        service.MakeTokenGetReady();
    }
    
    public IToken Token { get; private set; }

    public async Task TryCall(Func<IToken, Task> action)
    {
        var tries = 0;
        
        if (Token == null || Token.IsExpired) 
            Token = _service.GetToken(this);

        while (++tries <= 2)
        {
            try
            {
                await action(Token);
                break;
            }
            catch (Exception ex)
            {
                _ = _service.ReleaseToken();
                Token = _service.GetToken(this);
            }
        }
    }

    public void Dispose()
    {
        _service.ReleaseSession(this);
    }
}