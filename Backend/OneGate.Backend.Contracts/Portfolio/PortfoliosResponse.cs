using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Shared.Models.Portfolio;

namespace OneGate.Backend.Contracts.Portfolio
{
    [EntityName("response.portfolios")]
    public class PortfoliosResponse
    {
        public IEnumerable<PortfolioDto> Portfolios { get; set; }
    }
}