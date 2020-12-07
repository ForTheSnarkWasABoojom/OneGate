using MassTransit.Topology;

namespace OneGate.Backend.Transport.Contracts.Portfolio
{
    [EntityName("request.portfolio.delete")]
    public class DeletePortfolio
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
    }
}