using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneGate.Backend.Core.Shared.Api.Contracts;

namespace OneGate.Backend.Core.Timeseries.Api.Contracts.Layer
{
    public class FilterLayersDto : FilterDto
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "asset_id")]
        public int? AssetId { get; set; }

        [FromQuery(Name = "is_master")]
        [DefaultValue(true)]
        public bool? IsMaster { get; set; } = true;
    }
}