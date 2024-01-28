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
                    UniqueCode = 1234567890123,
                    Price = 999
                },
                new Product
                {
                    Id = 2,
                    Name = "Product 2",
                    UniqueCode = 9876543210987,
					Price = 999
				},
                new Product
                {
                    Id = 3,
                    Name = "Product 3",
                    UniqueCode = 1112223334445,
					Price = 999
				}
            );
			modelBuilder.Entity<Bill>().HasData(
	            new Bill
	            {
		            Id = 1,
					products = new List<Product>
		            {
			            new Product
			            {
				            Id = 1,
				            Name = "Product1",
				            UniqueCode = 123456,
				            Price = 10
			            },
			            new Product
			            {
				            Id = 2,
				            Name = "Product2",
				            UniqueCode = 789012,
				            Price = 20
			            },
                    }
	            }
            );

		}
		public DbSet<Bill> Bills { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
