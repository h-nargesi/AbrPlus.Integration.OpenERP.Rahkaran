using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class QuotationSlsRepository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<QuotationSls>(dbContext, loggerFactory), IQuotationSlsRepository
{
    public Task<string[]> GetAllIdsAsync()
    {
        return _context.QuotationSls3.Select(i => i.QuotationId.ToString()).ToArrayAsync();
    }

    public async Task<byte[]> GetMaxRowVersionAsync()
    {
        return await _context.QuotationSls3
            .OrderByDescending(x => x.Version)
            .Select(x => x.Version)
            .FirstOrDefaultAsync();
    }

    public async Task<long[]> GetLastChangesAsync(byte[] timestamp)
    {
        return await _context.QuotationSls3
            .Where(x => StructuralComparisons.StructuralComparer.Compare(x.Version, timestamp) > 0)
            .Select(x => x.QuotationId)
            .ToArrayAsync();
    }
}
