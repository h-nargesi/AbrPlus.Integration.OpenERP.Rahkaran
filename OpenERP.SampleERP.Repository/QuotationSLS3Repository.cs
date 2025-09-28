using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class QuotationSLS3Repository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<QuotationSLS3>(dbContext, loggerFactory), IQuotationSLS3Repository
{
    public Task<string[]> GetAllIdsAsync()
    {
        return _context.QuotationSLS3.Select(i => i.QuotationId.ToString()).ToArrayAsync();
    }

    public async Task<byte[]> GetMaxRowVersionAsync()
    {
        return await _context.QuotationSLS3
            .OrderByDescending(x => x.Version)
            .Select(x => x.Version)
            .FirstOrDefaultAsync();
    }
}
