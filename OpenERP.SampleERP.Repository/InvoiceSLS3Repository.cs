using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class InvoiceSLS3Repository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<InvoiceSLS3>(dbContext, loggerFactory), IInvoiceSLS3Repository
{
    public Task<string[]> GetAllIdsAsync()
    {
        return _context.InvoiceSLS3.Select(i => i.InvoiceId.ToString()).ToArrayAsync();
    }

    public async Task<byte[]> GetMaxRowVersionAsync()
    {
        return await _context.InvoiceSLS3
            .OrderByDescending(x => x.Version)
            .Select(x => x.Version)
            .FirstOrDefaultAsync();
    }
}
