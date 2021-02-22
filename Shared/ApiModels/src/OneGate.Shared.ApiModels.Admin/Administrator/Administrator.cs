using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Admin.Administrator
{
    public class Administrator
    {
        [MaxLength(100)]
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}