using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Shared.Models.Layout;

namespace OneGate.Backend.Services.AssetService.Repository
{
    public interface ILayoutRepository
    {
        public Task<int> AddAsync(CreateLayoutDto model);
        public Task<LayoutDto> FindAsync(int id);
        public Task<IEnumerable<LayoutDto>> FilterAsync(LayoutFilterDto filter);
        public Task RemoveAsync(int id);
    }
}