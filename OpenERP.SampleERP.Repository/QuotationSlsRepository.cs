using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
}
