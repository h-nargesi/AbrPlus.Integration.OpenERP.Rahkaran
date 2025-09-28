using AbrPlus.Integration.OpenERP.SampleERP.Enums;
using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using AbrPlus.Integration.OpenERP.Service;
using SeptaKit.Repository;
using Microsoft.Extensions.Options;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service
{
    public interface IRahkaranCompanyService : ICompanyService<RahkaranVersion, RahkaranCompanyConfig>
    {
        IOptions<ConnectionStringOption> GetConnectionStringOption();
    }
}
