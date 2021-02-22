using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using OneGate.Shared.ApiModels.Base;

namespace OneGate.Shared.ApiModels.Admin.Administrator
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