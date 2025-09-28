using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class PartyRepository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<Party>(dbContext, loggerFactory), IPartyRepository
{
    public Task<long[]> GetAllIdsAsync()
    {
        return _context.Party.Select(i => i.PartyId).ToArrayAsync();
    }

    public async Task<byte[]> GetMaxRowVersionAsync()
    {
        return await _context.Party
            .OrderByDescending(x => x.Version)
            .Select(x => x.Version)
            .FirstOrDefaultAsync();
    }
}
