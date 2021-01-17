using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Portfolio;

namespace OneGate.Backend.Transport.Contracts.Portfolio
{
    [EntityName("response.portfolios")]
    public class PortfoliosResponse
    {
        public IEnumerable<PortfolioDto> Portfolios { get; set; }
    }
}