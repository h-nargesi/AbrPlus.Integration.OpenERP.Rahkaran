using AbrPlus.Integration.OpenERP.Hosting.Repository;
using Microsoft.Extensions.Logging;
using SeptaKit.Models;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository
{
    public abstract class BaseSampleErpRepository<TEntity, TKey> : BaseOpenErpApiRepository<SampleErpDbContext, TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSampleErpRepository(ISampleErpDbContext dbContext, ILoggerFactory loggerFactory)
            : base(dbContext as SampleErpDbContext, loggerFactory)
        {
        }
    }

    public abstract class BaseSampleErpRepository<TEntity> : BaseOpenErpApiRepository<SampleErpDbContext, TEntity> where TEntity : BaseEntity
    {
        protected BaseSampleErpRepository(ISampleErpDbContext dbContext, ILoggerFactory loggerFactory)
            : base(dbContext as SampleErpDbContext, loggerFactory)
        {
        }
    }
}
