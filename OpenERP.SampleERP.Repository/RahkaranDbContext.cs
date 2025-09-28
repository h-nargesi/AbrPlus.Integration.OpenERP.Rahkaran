using AbrPlus.Integration.OpenERP.SampleERP.Models;
using AbrPlus.Integration.OpenERP.SampleERP.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SeptaKit.Repository.EFCore;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository;

public partial class RahkaranDbContext(IRahkaranCompanyService company, ILoggerFactory loggerFactory) :
    BaseSqlServerDbContext<RahkaranDbContext>(company.GetConnectionStringOption(), loggerFactory),
    IRahkaranDbContext
{
    public virtual DbSet<InvoiceRMS3> InvoiceRMS3 { get; set; }

    public virtual DbSet<InvoiceSLS3> InvoiceSLS3 { get; set; }

    public virtual DbSet<Party> Party { get; set; }

    public virtual DbSet<Payment> Payment { get; set; }

    public virtual DbSet<Product> Product { get; set; }

    public virtual DbSet<QuotationSLS3> QuotationSLS3 { get; set; }

    public virtual DbSet<Receipt> Receipt { get; set; }

    public virtual DbSet<SalesOrderRMS3> SalesOrderRMS3 { get; set; }

    protected override string MigrationTableSchema => "dbo";
    protected override string MigrationTableName => "__RahkaranMigrationHistory";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Party>(entity =>
        {
            entity.ToTable("Party", "GNR3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
