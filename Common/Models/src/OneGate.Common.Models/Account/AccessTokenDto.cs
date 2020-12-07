using Newtonsoft.Json;

namespace OneGate.Common.Models.Account
{
    public class AccessTokenDto
    {
        [JsonProperty("access_token")] 
        public string AccessToken { get; set; }
    }
}