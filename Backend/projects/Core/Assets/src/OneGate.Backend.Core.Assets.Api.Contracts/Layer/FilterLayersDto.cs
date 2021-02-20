using Newtonsoft.Json;
using OneGate.Backend.Core.Shared.Api.Contracts;

namespace OneGate.Backend.Core.Assets.Api.Contracts.Layer
{
    public class FilterLayersDto : FilterDto
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
    }
}