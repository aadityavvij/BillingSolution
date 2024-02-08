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
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<InvoiceProduct> InvoiceProducts { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.UseSerialColumns();
			SeedData(modelBuilder);
		}

		private void SeedData(ModelBuilder modelBuilder)
		{
			// Adding sample customers
			modelBuilder.Entity<Customer>().HasData(
				new Customer { CustomerId = 1, Name = "John Doe", PhNo = 1234567890, email = "john@example.com" },
				new Customer { CustomerId = 2, Name = "Jane Smith", PhNo = 9876543210, email = "jane@example.com" }
			);

			// Adding sample products
			modelBuilder.Entity<Product>().HasData(
				new Product { ProductId = 1, Name = "Product A", Price = 10.50m },
				new Product { ProductId = 2, Name = "Product B", Price = 20.75m },
				new Product { ProductId = 3, Name = "Product C", Price = 20.75m }
			);

			// Adding sample invoices
			modelBuilder.Entity<Invoice>().HasData(
				new Invoice { InvoiceId = 1, CustomerId = 1 },
				new Invoice { InvoiceId = 2, CustomerId = 2 }
			);

			// Adding sample invoice products
			modelBuilder.Entity<InvoiceProduct>().HasData(
				new InvoiceProduct { Id = 1, InvoiceId = 1, ProductId = 1 },
				new InvoiceProduct { Id = 2, InvoiceId = 1, ProductId = 2 },
				new InvoiceProduct { Id = 3, InvoiceId = 2, ProductId = 3 }
			);
		}

	}
}
