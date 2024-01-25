using System.ComponentModel.DataAnnotations;

namespace Mye_CommerceApp.Dtos
{
    public class OrderDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Shipping address is required.")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please enter a valid numeric contact number.")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }
    }
}
