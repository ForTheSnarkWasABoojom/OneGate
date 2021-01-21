using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Account
{
    public class AuthModel
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