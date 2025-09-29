using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class PaymentRepository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<Payment>(dbContext, loggerFactory), IPaymentRepository
{
    protected override DbSet<Payment> EntityDbSet => _context.Payment;

    protected override string EntityName => "%ReceiptAndPayment.Payment%";
}
