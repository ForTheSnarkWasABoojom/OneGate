using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Common.Models.Portfolio;

namespace OneGate.Backend.Core.AccountService.Repository
{
    public interface IPortfolioRepository
    {
        public Task<int> AddAsync(CreatePortfolioDto model);
        public Task<IEnumerable<PortfolioDto>> FilterAsync(PortfolioFilterDto filter, int ownerId);
        public Task RemoveAsync(int id, int ownerId);
    }
}