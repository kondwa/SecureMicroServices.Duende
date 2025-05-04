using System.ComponentModel.DataAnnotations;

namespace ID.Serve.Models
{
    public class UserCredentialsDto
    {
        
        public string? Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
