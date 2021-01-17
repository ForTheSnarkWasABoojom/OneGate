using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiContracts.Account
{
    public class OAuthDto
    {
        [Required]
        [MaxLength(100)]
        [JsonProperty("username")]
        public string Username { get; set; }
        
        [Required]
        [MaxLength(100)]
        [JsonProperty("password")]
        public string Password { get; set; }
        
        [Required]
        [MaxLength(100)]
        [JsonProperty("client_fingerprint")]
        public string ClientFingerprint { get; set; }
    }
}