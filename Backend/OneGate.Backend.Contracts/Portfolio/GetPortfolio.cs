using MassTransit.Topology;
using OneGate.Shared.Models.Portfolio;

namespace OneGate.Backend.Contracts.Portfolio
{
    [EntityName("request.portfolio.get")]
    public class GetPortfolios
    {
        public PortfolioFilterDto Filter { get; set; }
        public int OwnerId { get; set; }
    }
}