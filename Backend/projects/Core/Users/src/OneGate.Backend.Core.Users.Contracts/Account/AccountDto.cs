using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Contracts.Account
{
    public class AccountDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}