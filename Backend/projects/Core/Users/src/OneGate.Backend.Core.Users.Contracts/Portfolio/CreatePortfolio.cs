using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Users.Contracts.Portfolio
{
    [EntityName("request.portfolio.create")]
    public class CreatePortfolio
    {
        [JsonProperty("portfolio")]
        public PortfolioDto Portfolio { get; set; }
    }
}