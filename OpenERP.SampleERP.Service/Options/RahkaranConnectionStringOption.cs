using AbrPlus.Integration.OpenERP.SampleERP.Service.Configuration;
using AbrPlus.Integration.OpenERP.Service;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SeptaKit.Repository;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Options;

public class RahkaranConnectionStringOption(ICompanyContext companyContext, IRahkaranErpCompanyOptionStorageService rahkaranErpCompanyOptionStorageService) :
    IOptions<ConnectionStringOption>
{
    public ConnectionStringOption Value
    {
        get
        {
            var option = rahkaranErpCompanyOptionStorageService.GetCompanyConfig(companyContext.CompanyId);
            var builder = new SqlConnectionStringBuilder(option.ConnecitonString)
            {
                TrustServerCertificate = true
            };
            return new ConnectionStringOption() { ConnectionString = builder.ConnectionString };
        }
    }
}
