using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Api.Contracts.Credentials
{
    public class AuthDto
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }
        
        [JsonProperty("is_admin")]
        public bool? IsAdmin { get; set; }
    }
}