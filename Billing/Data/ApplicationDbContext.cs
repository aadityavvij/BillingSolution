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
		}
	}
}
