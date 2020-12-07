using Newtonsoft.Json;

namespace OneGate.Common.Models.Common
{
    public class ResourceDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}