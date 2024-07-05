using System.ComponentModel.DataAnnotations;

namespace dev.Models
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a name for the order status.")]
        [StringLength(50, ErrorMessage = "The name is too long.")]
        public string Name { get; set; }
    }
}
