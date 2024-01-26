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
        [DisplayName("Unique Code")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Unique Code must be 13 digits")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Unique Code must contain only numbers")]
        public long UniqueCode { get; set; }
        //[NotMapped]
        //[DisplayName("Upload File")]
        //public IFormFile ImageFile { get; set; }
    }
}
