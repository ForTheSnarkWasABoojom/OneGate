using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Api.Contracts.Administrator
{
    public class AdministratorDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}