using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class InvoiceRmsRepository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<InvoiceRms>(dbContext, loggerFactory), IInvoiceRmsRepository
{
    public Task<string[]> GetAllIdsAsync()
    {
        return _context.InvoiceRms.Select(i => i.InvoiceId.ToString()).ToArrayAsync();
    }

    public async Task<byte[]> GetMaxRowVersionAsync()
    {
        return await _context.InvoiceRms
            .OrderByDescending(x => x.Version)
            .Select(x => x.Version)
            .FirstOrDefaultAsync();
    }
}
