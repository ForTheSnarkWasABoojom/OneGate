using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Account
{
    public class AccessTokenModel
    {
        [JsonProperty("token")] 
        public string Token { get; set; }
    }
}