using Billing.Models;
using Microsoft.EntityFrameworkCore;

namespace Billing.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    UniqueCode = 1234567890123
                },
                new Product
                {
                    Id = 2,
                    Name = "Product 2",
                    UniqueCode = 9876543210987
                },
                new Product
                {
                    Id = 3,
                    Name = "Product 3",
                    UniqueCode = 1112223334445
                }
            );
        }

        public DbSet<Product> Products { get; set; }
    }
}
