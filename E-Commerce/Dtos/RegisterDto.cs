using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\W).{6,}$",
                           ErrorMessage = "Password must contain at least 1 uppercase letter," +
                           "1 lowercase letter, 1 non-alphanumeric character, and be at least 6 characters long.")]
        public string Password { get; set; }
    }
}
