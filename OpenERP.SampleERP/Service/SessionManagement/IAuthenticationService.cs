using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

public interface IAuthenticationService
{
    Task<IToken> Login();

    Task Logout(string sessionId);
}
