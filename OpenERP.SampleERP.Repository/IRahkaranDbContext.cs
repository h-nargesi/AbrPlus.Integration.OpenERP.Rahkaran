using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using SeptaKit.Repository.EFCore;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public interface IRahkaranDbContext : IDbContext
{
    DbSet<Party> Party { get; }
}
