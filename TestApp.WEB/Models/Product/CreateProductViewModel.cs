using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestApp.WEB.Models.Product
{
    public class CreateProductViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayName("Price")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be positive")]
        public decimal Price { get; set; }
    }
}
