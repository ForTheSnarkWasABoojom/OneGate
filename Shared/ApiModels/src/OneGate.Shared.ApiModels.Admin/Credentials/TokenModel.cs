using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Admin.Credentials
{
    public class TokenModel
    {
        [JsonProperty("token")] 
        public string Token { get; set; }
    }
}