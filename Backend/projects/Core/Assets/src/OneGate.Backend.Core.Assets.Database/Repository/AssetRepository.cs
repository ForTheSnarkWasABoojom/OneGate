using OneGate.Backend.Core.Assets.Database.Models;
using OneGate.Backend.Core.Base.Database.Repository;

namespace OneGate.Backend.Core.Assets.Database.Repository
{
    public class AssetRepository : GenericRepository<Asset>, IAssetRepository
    {
        private readonly DatabaseContext _db;

        public AssetRepository(DatabaseContext db) : base(db, db.Assets)
        {
            _db = db;
        }
    }
}