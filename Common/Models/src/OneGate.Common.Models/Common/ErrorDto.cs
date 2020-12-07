using Newtonsoft.Json;

namespace OneGate.Common.Models.Common
{
    public class ErrorDto
    {
        [JsonProperty("message")] 
        public string Message { get; set; }
        
        [JsonProperty("details")] 
        public string Exception { get; set; }
    }
}