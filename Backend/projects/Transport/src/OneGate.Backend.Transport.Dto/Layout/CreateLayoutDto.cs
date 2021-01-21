using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Layout
{
    public class CreateLayoutDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}