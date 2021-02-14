using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Base
{
    public class ErrorModel
    {
        [JsonProperty("message")] 
        public string Message { get; set; }
        
        [JsonProperty("status_code")] 
        public int StatusCode { get; set; }
        
        [JsonProperty("details")] 
        public string Details { get; set; }
    }
}