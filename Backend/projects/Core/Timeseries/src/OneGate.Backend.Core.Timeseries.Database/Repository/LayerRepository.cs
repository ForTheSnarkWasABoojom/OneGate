using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database.Repository
{
    public class LayerRepository : GenericRepository<Layer>, ILayerRepository
    {
        private readonly DatabaseContext _db;

        public LayerRepository(DatabaseContext db) : base(db, db.Layers)
        {
            _db = db;
        }

        public async Task<Layer> FindMasterAsync(int assetId)
        {
            var entity = await _db.Layers.Where(p => p.IsMaster && p.AssetId == assetId).AsNoTracking()
                .FirstOrDefaultAsync();
            return entity;
        }
    }
}