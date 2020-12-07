using MassTransit.Topology;

namespace OneGate.Backend.Transport.Contracts.PortfolioAssetLink
{
    [EntityName("request.portfolio_asset_link.delete")]
    public class DeletePortfolioAssetLink
    {
        public int Id { get; set; }
    }
}