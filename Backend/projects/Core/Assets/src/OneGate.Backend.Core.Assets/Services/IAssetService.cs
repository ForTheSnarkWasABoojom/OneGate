using System.Threading.Tasks;
using OneGate.Backend.Core.Assets.Contracts.Asset;

namespace OneGate.Backend.Core.Assets.Services
{
    public interface IAssetService
    {
        public Task<AssetsResponse> GetAssetsAsync(GetAssets request);
    }
}