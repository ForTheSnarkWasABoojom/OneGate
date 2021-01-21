using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Dto.Common
{
    public class ResourceDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}