using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Unique Code")]
        public long UniqueCode { get; set; }
        [Required]    
        public int Price { get; set; }
	}
}
