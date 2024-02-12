using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Billing.Models
{
    public class Product
    {
        [Key]
		[DisplayName("Product Id")]
		public int ProductId { get; set; }
        [Required]
        public string StoreId { get; set; }
        [Required]
        [DisplayName("Product Code")]
        public int ProductCode { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public bool Sold { get; set; }
        public ICollection<InvoiceProduct> InvoiceProducts { get; set; }
	}
}
