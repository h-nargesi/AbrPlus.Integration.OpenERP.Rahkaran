using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public class ProductRepository(IRahkaranDbContext dbContext, ILoggerFactory loggerFactory) :
    BaseRahkaranRepository<Product>(dbContext, loggerFactory), IProductRepository
{
    protected override DbSet<Product> EntityDbSet => _context.Product;

    protected override string EntityName => "%General.Sales%";
}
