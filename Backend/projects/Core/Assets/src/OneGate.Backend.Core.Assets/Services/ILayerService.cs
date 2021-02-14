using System.Threading.Tasks;
using OneGate.Backend.Core.Assets.Contracts.Layer;

namespace OneGate.Backend.Core.Assets.Services
{
    public interface ILayerService
    {
        public Task<LayersResponse> GetLayersAsync(GetLayers request);
    }
}