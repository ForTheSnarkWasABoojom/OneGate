using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Common
{
    public class ResourceModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}