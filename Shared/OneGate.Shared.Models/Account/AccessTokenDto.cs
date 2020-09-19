using Newtonsoft.Json;

namespace OneGate.Shared.Models.Account
{
    public class AccessTokenDto
    {
        [JsonProperty("access_token")] 
        public string AccessToken { get; set; }
    }
}