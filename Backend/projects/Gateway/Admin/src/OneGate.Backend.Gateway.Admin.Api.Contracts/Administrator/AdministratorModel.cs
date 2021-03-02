using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Backend.Gateway.Admin.Api.Contracts.Administrator
{
    public class AdministratorModel
    {
        [MaxLength(100)]
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}