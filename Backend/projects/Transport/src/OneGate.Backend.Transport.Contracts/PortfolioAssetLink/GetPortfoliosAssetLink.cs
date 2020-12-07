using MassTransit.Topology;
using OneGate.Common.Models.PortfolioAssetLink;

namespace OneGate.Backend.Transport.Contracts.PortfolioAssetLink
{
    [EntityName("request.portfolio_asset_link.get")]
    public class GetPortfolioAssetLinks
    {
        public PortfolioAssetLinkFilterDto Filter { get; set; }
    }
}