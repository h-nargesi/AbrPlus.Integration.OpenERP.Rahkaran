using AbrPlus.Integration.OpenERP.SampleERP.Models;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public interface IRahkaranAuthenticationService
{
    public Task<SessionInfo> Login();

    public Task Logout(string sessionId);
}
