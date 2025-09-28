using AbrPlus.Integration.OpenERP.Hosting.Repository;
using Microsoft.Extensions.Logging;
using SeptaKit.Models;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public abstract class BaseRahkaranRepository<TEntity, TKey>(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseOpenErpApiRepository<RahkaranDbContext, TEntity, TKey>(dbContext as RahkaranDbContext, loggerFactory)
    where TEntity : BaseEntity<TKey>
{
}

public abstract class BaseRahkaranRepository<TEntity>(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) : 
    BaseOpenErpApiRepository<RahkaranDbContext, TEntity>(dbContext as RahkaranDbContext, loggerFactory)
    where TEntity : BaseEntity
{
}
