using System.ComponentModel.DataAnnotations;

namespace ID.Serve.Models
{
    public class UserProfileDto
    {
        public string? Id { get; set; }
        [Required()]
        public string FirstName { get; set; } = string.Empty;
        [Required()]
        public string LastName { get; set; } = string.Empty;
        [Required()]
        public string UserName { get; set; } = string.Empty;
        [Required()]
        [EmailAddress()]
        public string Email { get; set; } = string.Empty;
        [Required()]
        [Phone()]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
