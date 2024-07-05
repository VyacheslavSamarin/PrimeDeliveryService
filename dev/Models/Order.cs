using System.ComponentModel.DataAnnotations;

namespace dev.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Please provide a customer ID.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please provide the products for the order.")]
        public List<Product> Products { get; set; }

        [Required(ErrorMessage = "Please provide an order status.")]
        [StringLength(50, ErrorMessage = "The order status is too long.")]
        public string OrderStatus { get; set; }

        [Required(ErrorMessage = "Please provide an order date.")]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Please provide a delivery date.")]
        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(Order), "ValidateDeliveryDate")]
        public DateTime DeliveryDate { get; set; }

        [Required(ErrorMessage = "Please provide an address.")]
        [StringLength(200, ErrorMessage = "The address is too long.")]
        public string Address { get; set; }

        public static ValidationResult ValidateDeliveryDate(DateTime deliveryDate, ValidationContext context)
        {
            var order = (Order)context.ObjectInstance;
            if (deliveryDate < order.OrderDate)
            {
                return new ValidationResult("Delivery date cannot be earlier than order date.");
            }
            return ValidationResult.Success;
        }
    }
}
