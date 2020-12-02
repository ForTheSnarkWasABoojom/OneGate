using MassTransit.Topology;
using OneGate.Shared.Models.PortfolioAssetLink;

namespace OneGate.Backend.Contracts.PortfolioAssetLink
{
    [EntityName("request.portfolio_asset_link.create")]
    public class CreatePortfolioAssetLink
    {
        public CreatePortfolioAssetLinkDto PortfolioAssetLink  { get; set; }
    }
}