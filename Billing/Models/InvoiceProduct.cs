using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Models
{
	public class InvoiceProduct
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("Invoice")]
		public long InvoiceId { get; set; }
		public Invoice Invoice { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}
