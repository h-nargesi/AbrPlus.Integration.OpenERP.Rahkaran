using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class ReceiptRepository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<Receipt>(dbContext, loggerFactory), IReceiptRepository
{
    protected override DbSet<Receipt> EntityDbSet => _context.Receipt;

    protected override string EntityName => "%ReceiptAndPayment.Receipt%";
}
