using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using OneGate.Shared.ApiModels.Base;

namespace OneGate.Shared.ApiModels.User.Credentials
{
    public class AuthRequest : UnauthorizedRequest
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