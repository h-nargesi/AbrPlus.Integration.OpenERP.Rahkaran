using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public interface IRahkaranAuthenticationService
{
    public Task<string> Login();

    public Task Logout(string sessionId);
}
