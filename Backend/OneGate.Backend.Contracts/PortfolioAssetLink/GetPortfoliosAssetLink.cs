using MassTransit.Topology;
using OneGate.Shared.Models.PortfolioAssetLink;

namespace OneGate.Backend.Contracts.PortfolioAssetLink
{
    [EntityName("request.portfolio_asset_link.get")]
    public class GetPortfolioAssetLinks
    {
        public PortfolioAssetLinkFilterDto Filter { get; set; }
    }
}