using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Database.Repository
{
    public interface IPorfolioAssetLinkRepository
    {
        public Task<int> AddAsync(PortfolioAssetLink model);

        public Task<IEnumerable<PortfolioAssetLink>> FilterAsync(int? id, int? portfolioId, int? assetId,
            int shift, int count);

        public Task RemoveAsync(int id);
    }
}