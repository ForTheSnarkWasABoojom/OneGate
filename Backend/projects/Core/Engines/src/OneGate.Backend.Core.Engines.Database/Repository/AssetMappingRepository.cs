using OneGate.Backend.Core.Engines.Database.Models;
using OneGate.Backend.Core.Shared.Database.Repository;

namespace OneGate.Backend.Core.Engines.Database.Repository
{
    public class AssetMappingRepository : GenericRepository<AssetMapping>, IAssetMappingRepository
    {
        private readonly DatabaseContext _db;

        public AssetMappingRepository(DatabaseContext db) : base(db, db.AssetMappings)
        {
            _db = db;
        }
    }
}