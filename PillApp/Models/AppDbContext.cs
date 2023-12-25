using Microsoft.EntityFrameworkCore;
using PillApp.Models;

namespace PillApp.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductCustomer> ProductCustomers { get; set; }
        //public DbSet<Bill> Bills { get; set; }
        public DbSet<BillVM> BillVMs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCustomer>()
                .HasKey(x=> new { x.ProductId, x.CustomerId});
        }

        public DbSet<PillApp.Models.BillVM> BillViewModel { get; set; }
    }
}
