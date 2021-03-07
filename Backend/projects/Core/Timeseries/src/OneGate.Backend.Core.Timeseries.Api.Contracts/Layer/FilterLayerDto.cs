using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Core.Shared.Api.Contracts;

namespace OneGate.Backend.Core.Timeseries.Api.Contracts.Layer
{
    public class FilterLayerDto : FilterDto
    {
        [FromQuery(Name = "owner_id")]
        public int OwnerId { get; set; }
        
        [FromQuery(Name = "asset_id")]
        public int AssetId { get; set; }
        
        [FromQuery(Name = "interval")]
        public string Interval { get; set; }
    }
}