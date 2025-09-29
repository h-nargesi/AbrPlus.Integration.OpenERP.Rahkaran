using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class SalesOrderRmsRepository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<SalesOrderRms>(dbContext, loggerFactory), ISalesOrderRMS3Repository
{
    protected override DbSet<SalesOrderRms> EntityDbSet => _context.SalesOrderRms;

    protected override string EntityName => "%Retail.RetailDocuments/سفارش خرده فروشی%";
}
