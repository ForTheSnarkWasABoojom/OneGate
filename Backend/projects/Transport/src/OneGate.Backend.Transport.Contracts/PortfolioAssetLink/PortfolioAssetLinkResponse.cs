using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Common.Models.PortfolioAssetLink;

namespace OneGate.Backend.Transport.Contracts.PortfolioAssetLink
{
    [EntityName("response.portfolio_asset_links")]
    public class PortfolioAssetLinksResponse
    {
        public IEnumerable<PortfolioAssetLinkDto> PortfolioAssetLinks { get; set; }
    }
}