using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Contracts
{
    public abstract class LargeDataRequest
    {
        [JsonProperty("count")]
        public int? Count { get; set; }
        
        [JsonProperty("shift")]
        public int? Shift { get; set; }
    }
}