using System.Collections.Generic;
using System.Threading.Tasks;
using OneGate.Backend.Core.Records.Database.Models;

namespace OneGate.Backend.Core.Records.Database.Repository
{
    public interface IAssetRepository
    {
        public Task<int> AddAsync(Asset model);

        public Task<IEnumerable<Asset>> FilterAsync(int? id, string type, string ticker, int? exchangeId,
            string exchangeTitle, string exchangeEngineType, int shift, int count);

        public Task RemoveAsync(int id);
    }
}