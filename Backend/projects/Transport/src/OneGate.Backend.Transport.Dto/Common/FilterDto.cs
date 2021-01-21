using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Common
{
    public class FilterDto
    {
        [JsonProperty("shift")]
        public int Shift { get; set; } = 0;
        
        [JsonProperty("count")]
        public int Count { get; set; } = 1;
    }
}