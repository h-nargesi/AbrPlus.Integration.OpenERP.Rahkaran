using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class QuotationSlsRepository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<QuotationSls>(dbContext, loggerFactory), IQuotationSlsRepository
{
    protected override DbSet<QuotationSls> EntityDbSet => _context.QuotationSls;

    protected override string EntityName => "%General.Sales%";
}
