using Newtonsoft.Json;

namespace OneGate.Backend.Gateway.Shared.Api.Contracts
{
    public class ErrorResponse
    {
        [JsonProperty("message")] 
        public string Message { get; set; }
        
        [JsonProperty("status_code")] 
        public int StatusCode { get; set; }
        
        [JsonProperty("details")] 
        public string Details { get; set; }
    }
}