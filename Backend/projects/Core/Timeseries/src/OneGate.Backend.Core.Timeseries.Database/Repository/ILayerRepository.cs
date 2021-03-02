using System.Threading.Tasks;
using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database.Repository
{
    public interface ILayerRepository : IRepository<Layer>
    {
        public Task<Layer> FindMasterAsync(int assetId);
    }
}