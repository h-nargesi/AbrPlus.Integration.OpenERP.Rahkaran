using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class ReceiptRepository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<Receipt>(dbContext, loggerFactory), IReceiptRepository
{
    public Task<string[]> GetAllIdsAsync()
    {
        return _context.Receipt.Select(i => i.ReceiptId.ToString()).ToArrayAsync();
    }

    public async Task<byte[]> GetMaxRowVersionAsync()
    {
        return await _context.Receipt
            .OrderByDescending(x => x.Version)
            .Select(x => x.Version)
            .FirstOrDefaultAsync();
    }
}
