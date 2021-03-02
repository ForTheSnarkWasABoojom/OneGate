using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using OneGate.Backend.Gateway.Shared.Api.Contracts;

namespace OneGate.Backend.Gateway.Admin.Api.Contracts.Credentials
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