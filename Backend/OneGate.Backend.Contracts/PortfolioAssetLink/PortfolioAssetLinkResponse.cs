using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Shared.Models.PortfolioAssetLink;

namespace OneGate.Backend.Contracts.PortfolioAssetLink
{
    [EntityName("response.portfolio_asset_links")]
    public class PortfolioAssetLinksResponse
    {
        public IEnumerable<PortfolioAssetLinkDto> PortfolioAssetLinks { get; set; }
    }
}