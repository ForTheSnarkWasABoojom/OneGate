using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Portfolio;

namespace OneGate.Backend.Transport.Contracts.Portfolio
{
    [EntityName("request.portfolio.get")]
    public class GetPortfolios
    {
        public PortfolioFilterDto Filter { get; set; }
        public int OwnerId { get; set; }
    }
}