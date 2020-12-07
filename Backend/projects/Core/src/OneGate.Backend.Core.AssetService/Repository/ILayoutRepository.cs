using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Common.Models.Layout;

namespace OneGate.Backend.Core.AssetService.Repository
{
    public interface ILayoutRepository
    {
        public Task<int> AddAsync(CreateLayoutDto model);
        public Task<LayoutDto> FindAsync(int id);
        public Task<IEnumerable<LayoutDto>> FilterAsync(LayoutFilterDto filter);
        public Task RemoveAsync(int id);
    }
}