using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Contracts.Asset;
using OneGate.Shared.Models.Asset;

namespace OneGate.Backend.Services.AssetService.Repository
{
    public interface IAssetRepository
    {
        public Task<int> AddAsync(CreateAssetBaseDto model);
        public Task<AssetBaseDto> FindAsync(int id);
        public Task<IEnumerable<AssetBaseDto>> FilterAsync(AssetBaseFilterDto filter);
        public Task RemoveAsync(int id);
    }
}