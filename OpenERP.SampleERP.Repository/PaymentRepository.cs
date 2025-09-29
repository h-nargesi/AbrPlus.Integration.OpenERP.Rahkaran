using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class PaymentRepository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<Payment>(dbContext, loggerFactory), IPaymentRepository
{
    public Task<string[]> GetAllIdsAsync()
    {
        return _context.Payment.Select(i => i.PaymentId.ToString()).ToArrayAsync();
    }

    public async Task<byte[]> GetMaxRowVersionAsync()
    {
        return await _context.Payment
            .OrderByDescending(x => x.Version)
            .Select(x => x.Version)
            .FirstOrDefaultAsync();
    }

    public async Task<long[]> GetLastChangesAsync(byte[] timestamp)
    {
        return await _context.Payment
            .Where(x => StructuralComparisons.StructuralComparer.Compare(x.Version, timestamp) > 0)
            .Select(x => x.PaymentId)
            .ToArrayAsync();
    }
}
