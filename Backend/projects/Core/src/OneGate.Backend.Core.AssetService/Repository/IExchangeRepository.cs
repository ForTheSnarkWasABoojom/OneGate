using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Common.Models.Exchange;

namespace OneGate.Backend.Core.AssetService.Repository
{
    public interface IExchangeRepository
    {
        public Task<int> AddAsync(CreateExchangeDto model);
        public Task<ExchangeDto> FindAsync(int id);
        public Task<IEnumerable<ExchangeDto>> FilterAsync(ExchangeFilterDto filter);
        public Task RemoveAsync(int id);
    }
}