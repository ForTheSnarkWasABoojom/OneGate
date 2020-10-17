using Newtonsoft.Json;

namespace OneGate.Shared.Models.Common
{
    public class ResourceDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}