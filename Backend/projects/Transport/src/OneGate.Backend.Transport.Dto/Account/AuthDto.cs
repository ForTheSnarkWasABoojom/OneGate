using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Account
{
    public class AuthDto
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}