using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.Models.Common;
using OneGate.Shared.Models.Exchange;

namespace OneGate.Shared.Models.Asset
{
    public class AssetFilterDto : FilterDto
    {
        [FromQuery(Name = "id")]
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "type")]
        [JsonProperty("type")]
        public AssetTypeDto? Type { get; set; }

        [MaxLength(30)]
        [FromQuery(Name = "ticker")]
        [JsonProperty("ticker")]
        public string Ticker { get; set; }
        
        [FromQuery(Name = "exchange")]
        [JsonProperty("exchange")]
        public ExchangeFilterDto Exchange { get; set; }
    }
}