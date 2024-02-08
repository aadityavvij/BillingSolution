using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Billing.Models
{
	public class Customer
	{
		[Key]
		public int CustomerId { get; set; }
		[Required]
		[DisplayName("Name")]
		public string Name { get; set; }
		[Required]
		[DisplayName("Phone Number")]
		public long PhNo { get; set; }
		[Required]
		[DisplayName("Email Id")]
		public string email { get; set; }
		public ICollection<Invoice> Invoices { get; set; }
	}
}
