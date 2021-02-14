using System.Collections.Generic;
using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Contracts.Portfolio
{
    [EntityName("response.portfolios")]
    public class PortfoliosResponse
    {
        [JsonProperty("portfolios")]
        public IEnumerable<PortfolioDto> Portfolios { get; set; }
    }
}