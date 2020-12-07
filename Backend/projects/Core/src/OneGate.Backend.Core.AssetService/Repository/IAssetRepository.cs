using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Common.Models.Asset;

namespace OneGate.Backend.Core.AssetService.Repository
{
    public interface IAssetRepository
    {
        public Task<int> AddAsync(CreateAssetDto model);
        public Task<AssetDto> FindAsync(int id);
        public Task<IEnumerable<AssetDto>> FilterAsync(AssetFilterDto filter);
        public Task RemoveAsync(int id);
    }
}