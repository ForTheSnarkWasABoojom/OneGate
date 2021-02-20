using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Admin.Credentials
{
    public class TokenResponse
    {
        [JsonProperty("access_token")] 
        public string AccessToken { get; set; }
    }
}