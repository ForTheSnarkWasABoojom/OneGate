using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Database.Repository
{
    public interface IPortfolioRepository
    {
        public Task<int> AddAsync(Portfolio model);

        public Task<IEnumerable<Portfolio>> FilterAsync(int? id, int? ownerId, string name,
            int shift, int count);

        public Task RemoveAsync(int id, int ownerId);
    }
}