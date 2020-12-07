using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Common.Models.PortfolioAssetLink;

namespace OneGate.Backend.Core.AccountService.Repository
{
    public interface IPorfolioAssetLinkRepository
    {
        public Task<int> AddAsync(CreatePortfolioAssetLinkDto model);
        public Task<IEnumerable<PortfolioAssetLinkDto>> FilterAsync(PortfolioAssetLinkFilterDto filter);
        public Task RemoveAsync(int id);
    }
}