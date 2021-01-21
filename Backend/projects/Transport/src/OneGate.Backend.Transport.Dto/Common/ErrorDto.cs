using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Common
{
    public class ErrorDto
    {
        [JsonProperty("message")] 
        public string Message { get; set; }
    }
}