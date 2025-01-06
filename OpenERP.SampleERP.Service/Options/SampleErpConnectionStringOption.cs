using AbrPlus.Integration.OpenERP.SampleERP.Service.Configuration;
using AbrPlus.Integration.OpenERP.Service;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SeptaKit.Repository;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Options
{
    public class SampleErpConnectionStringOption : IOptions<ConnectionStringOption>
    {
        private readonly ICompanyContext _companyContext;
        private readonly ISampleErpCompanyOptionStorageService _sampleErpCompanyOptionStorageService;

        public SampleErpConnectionStringOption(ICompanyContext companyContext, ISampleErpCompanyOptionStorageService sampleErpCompanyOptionStorageService)
        {
            _companyContext = companyContext;
            _sampleErpCompanyOptionStorageService = sampleErpCompanyOptionStorageService;
        }
        public ConnectionStringOption Value
        {
            get
            {
                var option = _sampleErpCompanyOptionStorageService.GetCompanyConfig(_companyContext.CompanyId);
                var builder = new SqlConnectionStringBuilder(option.ConnecitonString);
                builder.TrustServerCertificate = true;
                return new ConnectionStringOption() { ConnectionString = builder.ConnectionString };
            }
        }
    }
}
