using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Billing.Models
{
	public class Bill
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public ICollection<Product> Products { get; } = new List<Product>();
	}
}