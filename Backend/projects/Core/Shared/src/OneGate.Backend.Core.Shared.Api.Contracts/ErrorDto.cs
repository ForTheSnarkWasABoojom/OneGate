using Newtonsoft.Json;

namespace OneGate.Backend.Core.Shared.Api.Contracts
{
    public class ErrorDto
    {
        [JsonProperty("message")] 
        public string Message { get; set; }
        
        [JsonProperty("status_code")] 
        public int StatusCode { get; set; }
        
        [JsonProperty("details")] 
        public string Details { get; set; }
    }
}