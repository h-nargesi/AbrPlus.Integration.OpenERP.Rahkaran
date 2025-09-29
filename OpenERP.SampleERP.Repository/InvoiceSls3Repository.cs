using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class InvoiceSls3Repository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<InvoiceSls>(dbContext, loggerFactory), IInvoiceSLS3Repository
{
    protected override DbSet<InvoiceSls> EntityDbSet => _context.InvoiceSls;

    protected override string EntityName => "%General.PartyManagement/Ma%";
}
