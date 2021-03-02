using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using OneGate.Backend.Gateway.Shared.Api.Contracts;

namespace OneGate.Backend.Gateway.Admin.Api.Contracts.Administrator
{
    public class CreateAdministratorRequest : UnauthorizedRequest
    {
        [MaxLength(100)]
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(100)]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}