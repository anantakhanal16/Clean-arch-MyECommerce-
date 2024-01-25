using System.ComponentModel.DataAnnotations;

namespace Mye_CommerceApp.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
        
        public string? Role { get; set; }
    }
}
