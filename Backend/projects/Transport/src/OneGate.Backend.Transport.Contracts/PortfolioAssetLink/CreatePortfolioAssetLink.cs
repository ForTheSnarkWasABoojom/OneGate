using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.PortfolioAssetLink;

namespace OneGate.Backend.Transport.Contracts.PortfolioAssetLink
{
    [EntityName("request.portfolio_asset_link.create")]
    public class CreatePortfolioAssetLink
    {
        public CreatePortfolioAssetLinkDto PortfolioAssetLink { get; set; }
    }
}