using Newtonsoft.Json;

namespace OneGate.Shared.ApiContracts.Common
{
    public class ResourceDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}