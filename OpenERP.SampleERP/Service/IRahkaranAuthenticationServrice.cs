using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public interface IRahkaranAuthenticationServrice
{
    public Task<string> Login();

    public Task Logout(string sessionId);
}
