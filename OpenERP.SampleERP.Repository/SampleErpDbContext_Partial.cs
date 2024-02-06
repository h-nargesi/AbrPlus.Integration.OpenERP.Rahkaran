using AbrPlus.Integration.OpenERP.SampleERP.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SeptaKit.Repository;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository
{
    public partial class SampleErpDbContext : ISampleErpDbContext
    {
        protected override string MigrationTableSchema => "dbo";
        protected override string MigrationTableName => "__SampleErpMigrationHistory";


        public SampleErpDbContext(IOptions<ConnectionStringOption> options, ILoggerFactory loggerFactory)
            :base(options, loggerFactory)
        {

        }        
    }
}
