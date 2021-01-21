using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneGate.Shared.ApiModels.Common;
using OneGate.Shared.ApiModels.Exchange;

namespace OneGate.Shared.ApiModels.Asset
{
    public class AssetFilterModel : FilterModel
    {
        [FromQuery(Name = "id")]
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [FromQuery(Name = "type")]
        [JsonProperty("type")]
        public AssetTypeModel? Type { get; set; }

        [MaxLength(30)]
        [FromQuery(Name = "ticker")]
        [JsonProperty("ticker")]
        public string Ticker { get; set; }
        
        [FromQuery(Name = "exchange")]
        [JsonProperty("exchange")]
        public ExchangeFilterModel Exchange { get; set; }
    }
}