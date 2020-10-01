using Newtonsoft.Json;

namespace OneGate.Shared.Models.Common
{
    public class CreatedResourceDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}