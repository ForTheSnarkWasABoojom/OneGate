using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Records.Database.Models;

namespace OneGate.Backend.Core.Records.Database.Repository
{
    public interface ILayoutRepository
    {
        public Task<int> AddAsync(Layout model);
        public Task<IEnumerable<Layout>> FilterAsync(int? id, string name, int shift, int count);
        public Task RemoveAsync(int id);
    }
}