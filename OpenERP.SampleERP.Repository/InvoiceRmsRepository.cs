using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class InvoiceRmsRepository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<InvoiceRms>(dbContext, loggerFactory), IInvoiceRmsRepository
{
    protected override DbSet<InvoiceRms> EntityDbSet => _context.InvoiceRms;

    protected override string EntityName => "%Retail.RetailDocuments/فاکتور خرده فروشی%";
}
