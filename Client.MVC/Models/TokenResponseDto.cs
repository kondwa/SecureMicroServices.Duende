using Newtonsoft.Json;

namespace Client.MVC.Models
{
    public class TokenResponseDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; } = string.Empty;

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;

        [JsonProperty("scope")]
        public string Scope { get; set; } = string.Empty;
        [JsonProperty("id_token")]
        public string IdToken { get; set; } = string.Empty;
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; } = string.Empty;
        [JsonProperty("error_uri")]
        public string ErrorCode { get; set; } = string.Empty;
        [JsonProperty("state")]
        public string State { get; set; } = string.Empty;
        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;
        [JsonProperty("session_state")]
        public string SessionState { get; set; } = string.Empty;
        public bool IsError => !string.IsNullOrEmpty(Error) || !string.IsNullOrEmpty(ErrorDescription);
    }
}
