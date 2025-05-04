using System.ComponentModel.DataAnnotations;

namespace Client.MVC.Models
{
    public class UserCredentialsDto
    {
        public string? Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; } = string.Empty;
        public string GrantType { get; set; } = string.Empty;
        public string Scope { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public Dictionary<string,string> FormData => new Dictionary<string, string>()
        {
            { "grant_type", GrantType},
            { "scope", Scope },
            { "client_id", ClientId },
            { "client_secret", ClientSecret },
            { "username", UserName },
            { "password", Password }
        };
    }
}
