using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Common
{
    public class ErrorModel
    {
        [JsonProperty("message")] 
        public string Message { get; set; }
    }
}