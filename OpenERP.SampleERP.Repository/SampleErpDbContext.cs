using AbrPlus.Integration.OpenERP.SampleERP.Models;
using Microsoft.EntityFrameworkCore;
using SeptaKit.Repository.EFCore;

namespace AbrPlus.Integration.OpenERP.SampleERP.Repository
{
    public partial class SampleErpDbContext : BaseSqlServerDbContext<SampleErpDbContext>
    {
        public virtual DbSet<Customer> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Test", "dbo");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
