using System;
using System.Net;
using System.Threading.Tasks;
using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using AbrPlus.Integration.OpenERP.SampleERP.Shared;
using Refit;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

public class Session : ISession
{
    private readonly ITokenService _service;
    private readonly RahkaranCompanyConfig _config;

    public Session(ITokenService service, IRahkaranCompanyService company)
    {
        _service = service;
        _config = company.GetCompanyConfig();
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
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
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

    public T GetWebService<T>(string basePath)
    {
        return RestService.For<T>(_config.BaseUrl + basePath, JsonObjectExtension.RefitSettings);
    }

    public void Dispose()
    {
        _ = _service.ReleaseSession(this);
        GC.SuppressFinalize(this);
    }
}