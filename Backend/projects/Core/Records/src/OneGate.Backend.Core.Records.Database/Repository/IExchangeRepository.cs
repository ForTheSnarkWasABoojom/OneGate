using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Records.Database.Models;

namespace OneGate.Backend.Core.Records.Database.Repository
{
    public interface IExchangeRepository
    {
        public Task<int> AddAsync(Exchange model);

        public Task<IEnumerable<Exchange>> FilterAsync(int? id, string title, string engineType, int shift,
            int count);

        public Task RemoveAsync(int id);
    }
}