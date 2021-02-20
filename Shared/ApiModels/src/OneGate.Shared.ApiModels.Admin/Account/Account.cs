using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Admin.Account
{
    public class Account
    {
        [MaxLength(100)]
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [MaxLength(30)]
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        [MaxLength(30)]
        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}