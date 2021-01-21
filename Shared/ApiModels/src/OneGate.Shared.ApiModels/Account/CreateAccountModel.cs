using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Account
{
    public class CreateAccountModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(100)]
        [JsonProperty("password")]
        public string Password { get; set; }
        
        [Required]
        [MaxLength(30)]
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(30)]
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        
        [Required]
        [MaxLength(100)]
        [JsonProperty("client_fingerprint")]
        public string ClientFingerprint { get; set; }
    }
}