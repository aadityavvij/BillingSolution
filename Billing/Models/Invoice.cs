using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Models
{
	public class Invoice
	{
		[Key]
        public long InvoiceId { get; set; }
		[Required]
		[ForeignKey("Customer")]
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
		public ICollection<InvoiceProduct> InvoiceProducts { get; set; }

	}
}
