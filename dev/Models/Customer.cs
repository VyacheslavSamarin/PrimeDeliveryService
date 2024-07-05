using System.ComponentModel.DataAnnotations;

namespace dev.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Hey, please provide a name!")]
        [StringLength(20, ErrorMessage = "The name is too long.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Hey, please provide an address!")]
        public string Address { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Hey, please provide a phone number!")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "The phone number is not valid.")]
        public string Phone { get; set; }
    }
}
