using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

public interface ITokenService
{
    Task MakeTokenGetReady();

    IToken GetToken(ISession session);

    Task ReleaseSession(ISession session);
    
    Task ReleaseToken();
}