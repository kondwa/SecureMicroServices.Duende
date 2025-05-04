using System.ComponentModel.DataAnnotations;

namespace ID.Serve.Models
{
    public class UserRegisterDto
    {
        public string? Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
    ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        [EmailAddress()]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone()]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
