using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class InvoiceSls3Repository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<InvoiceSls>(dbContext, loggerFactory), IInvoiceSLS3Repository
{
    public Task<string[]> GetAllIdsAsync()
    {
        return _context.InvoiceSls3.Select(i => i.InvoiceId.ToString()).ToArrayAsync();
    }

    public async Task<byte[]> GetMaxRowVersionAsync()
    {
        return await _context.InvoiceSls3
            .OrderByDescending(x => x.Version)
            .Select(x => x.Version)
            .FirstOrDefaultAsync();
    }

    public async Task<long[]> GetLastChangesAsync(byte[] timestamp)
    {
        return await _context.InvoiceSls3
            .Where(x => StructuralComparisons.StructuralComparer.Compare(x.Version, timestamp) > 0)
            .Select(x => x.InvoiceId)
            .ToArrayAsync();
    }
}
