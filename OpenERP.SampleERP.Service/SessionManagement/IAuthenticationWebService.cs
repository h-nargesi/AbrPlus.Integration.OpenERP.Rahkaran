using Refit;
using System.Net.Http;
using System.Threading.Tasks;
using static AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement.AuthenticationHttpService;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

[Headers("Content-Type: application/json")]
public interface IAuthenticationWebService
{
    [Get("/session")]
    Task<AuthenticationSession> Session();

    [Post("/login")]
    Task<HttpResponseMessage> Login(object data);

    [Post("/loginssl")]
    Task<HttpResponseMessage> LoginSsl(object data);

    [Post("/logout")]
    Task<HttpResponseMessage> Logout(string sessionId);
}
