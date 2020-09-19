using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.Models.Common;

namespace OneGate.Shared.Models.Asset
{
    public class AssetBaseFilterDto : FilterBaseDto
    {
        [FromQuery(Name = "type")]
        [JsonProperty("type")]
        public AssetTypeDto? Type { get; set; }
        
        [FromQuery(Name = "exchange_id")]
        [JsonProperty("exchange_id")]
        public int? ExchangeId { get; set; }
        
        [MaxLength(30)]
        [FromQuery(Name = "ticker")]
        [JsonProperty("ticker")]
        public string Ticker { get; set; }
    }
}