using System.ComponentModel.DataAnnotations;

namespace Billing.Models
{
	public class Bill
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public List<Product> products { get; set; }
	}
}