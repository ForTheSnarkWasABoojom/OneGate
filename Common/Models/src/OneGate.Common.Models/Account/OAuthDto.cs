using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Common.Models.Account
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
    }
}