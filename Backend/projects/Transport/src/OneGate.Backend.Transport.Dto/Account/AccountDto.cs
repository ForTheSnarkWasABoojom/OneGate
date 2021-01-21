using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Account
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
        
        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }
    }
}