using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.User.Credentials
{
    public class TokenModel
    {
        [JsonProperty("token")] 
        public string Token { get; set; }
    }
}