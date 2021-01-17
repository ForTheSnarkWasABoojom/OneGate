using MassTransit.Topology;
using OneGate.Shared.ApiContracts.PortfolioAssetLink;

namespace OneGate.Backend.Transport.Contracts.PortfolioAssetLink
{
    [EntityName("request.portfolio_asset_link.create")]
    public class CreatePortfolioAssetLink
    {
        public CreatePortfolioAssetLinkDto PortfolioAssetLink { get; set; }
    }
}