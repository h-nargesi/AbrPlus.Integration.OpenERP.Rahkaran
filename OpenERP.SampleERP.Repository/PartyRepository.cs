using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class PartyRepository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<Party>(dbContext, loggerFactory), IPartyRepository
{
    protected override DbSet<Party> EntityDbSet => _context.Party;

    protected override string EntityName => "%General.PartyManagement/شخص/شرکت%";
}
