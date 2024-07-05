using System.ComponentModel.DataAnnotations;

namespace dev.Models

{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a product name.")]
        [StringLength(100, ErrorMessage = "The name is too long.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "The description is too long.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please provide a price for the product.")]
        [Range(0, int.MaxValue, ErrorMessage = "The price must be a positive number.")]
        public int Price { get; set; }
    }
}
