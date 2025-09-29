using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class ProductRepository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<Product>(dbContext, loggerFactory), IProductRepository
{
    public Task<string[]> GetAllIdsAsync()
    {
        return _context.Product.Select(i => i.ProductId.ToString()).ToArrayAsync();
    }

    public async Task<byte[]> GetMaxRowVersionAsync()
    {
        return await _context.Product
            .OrderByDescending(x => x.Version)
            .Select(x => x.Version)
            .FirstOrDefaultAsync();
    }

    public async Task<long[]> GetLastChangesAsync(byte[] timestamp)
    {
        return await _context.Product
            .Where(x => StructuralComparisons.StructuralComparer.Compare(x.Version, timestamp) > 0)
            .Select(x => x.ProductId)
            .ToArrayAsync();
    }
}
