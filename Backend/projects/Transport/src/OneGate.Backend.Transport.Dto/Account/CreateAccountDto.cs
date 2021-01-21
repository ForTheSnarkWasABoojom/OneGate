using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Account
{
    public class CreateAccountDto
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }
        
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        
        [JsonProperty("client_fingerprint")]
        public string ClientFingerprint { get; set; }
    }
}