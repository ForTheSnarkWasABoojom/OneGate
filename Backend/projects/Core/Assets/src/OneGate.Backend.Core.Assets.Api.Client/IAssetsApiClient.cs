using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Assets.Api.Contracts.Asset;
using OneGate.Backend.Core.Assets.Api.Contracts.Exchange;
using OneGate.Backend.Core.Assets.Api.Contracts.Layer;

namespace OneGate.Backend.Core.Assets.Api.Client
{
    public interface IAssetsApiClient
    {
        public Task<IEnumerable<AssetDto>> GetAssetsAsync(FilterAssetsDto request);
        public Task<IEnumerable<ExchangeDto>> GetExchangesAsync(FilterExchangesDto request);
        public Task<IEnumerable<LayersDto>> GetLayersAsync(FilterLayersDto request);
    }
}