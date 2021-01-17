using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Portfolio;

namespace OneGate.Backend.Transport.Contracts.Portfolio
{
    [EntityName("request.portfolio.create")]
    public class CreatePortfolio
    {
        public CreatePortfolioDto Portfolio { get; set; }
        public int OwnerId { get; set; }
    }
}