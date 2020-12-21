using MassTransit.Topology;
using OneGate.Common.Models.PortfolioAssetLink;

namespace OneGate.Backend.Transport.Contracts.PortfolioAssetLink
{
    [EntityName("request.portfolio_asset_link.create")]
    public class CreatePortfolioAssetLink
    {
        public CreatePortfolioAssetLinkDto PortfolioAssetLink { get; set; }
    }
}