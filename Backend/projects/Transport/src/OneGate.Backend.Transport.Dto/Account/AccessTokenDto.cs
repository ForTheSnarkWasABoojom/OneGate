using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Account
{
    public class AccessTokenDto
    {
        [JsonProperty("access_token")] 
        public string AccessToken { get; set; }
    }
}