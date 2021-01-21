using Newtonsoft.Json;
using OneGate.Backend.Transport.Dto.Common;
using OneGate.Backend.Transport.Dto.Exchange;

namespace OneGate.Backend.Transport.Dto.Asset
{
    public class AssetFilterDto : FilterDto
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [JsonProperty("type")]
        public AssetTypeDto? Type { get; set; }
        
        [JsonProperty("ticker")]
        public string Ticker { get; set; }
        
        [JsonProperty("exchange")]
        public ExchangeFilterDto Exchange { get; set; }
    }
}