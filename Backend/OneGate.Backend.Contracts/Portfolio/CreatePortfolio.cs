using MassTransit.Topology;
using OneGate.Shared.Models.Portfolio;

namespace OneGate.Backend.Contracts.Portfolio
{
    [EntityName("request.portfolio.create")]
    public class CreatePortfolio
    {
        public CreatePortfolioDto Portfolio  { get; set; }
        public int OwnerId { get; set; }
    }
}