using System.Collections.Generic;
using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Assets.Contracts.Exchange
{
    [EntityName("response.exchanges")]
    public class ExchangesResponse
    {
        [JsonProperty("exchanges")]
        public IEnumerable<ExchangeDto> Exchanges { get; set; }
    }
}