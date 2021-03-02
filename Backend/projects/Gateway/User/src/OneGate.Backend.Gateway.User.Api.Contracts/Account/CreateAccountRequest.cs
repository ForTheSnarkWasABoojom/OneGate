using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using OneGate.Backend.Gateway.Shared.Api.Contracts;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Account
{
    public class CreateAccountRequest : UnauthorizedRequest
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
    }
}