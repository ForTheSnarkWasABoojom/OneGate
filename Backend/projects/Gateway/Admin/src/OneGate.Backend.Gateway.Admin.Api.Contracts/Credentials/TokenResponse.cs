using Newtonsoft.Json;

namespace OneGate.Backend.Gateway.Admin.Api.Contracts.Credentials
{
    public class TokenResponse
    {
        [JsonProperty("access_token")] 
        public string AccessToken { get; set; }
    }
}