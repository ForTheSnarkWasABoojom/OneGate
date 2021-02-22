using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Core.Shared.Api.Contracts;

namespace OneGate.Backend.Core.Engines.Api.Contracts.AssetMapping
{
    public class FilterAssetMappingsDto : FilterDto
    {
        [FromQuery(Name = "id")]
        public int Id { get; set; }

        [FromQuery(Name = "engine_id")]
        public int EngineId { get; set; }

        [FromQuery(Name = "asset_id")]
        public int AssetId { get; set; }
    }
}