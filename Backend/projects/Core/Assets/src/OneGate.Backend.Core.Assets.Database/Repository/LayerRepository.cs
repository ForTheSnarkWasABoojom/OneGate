using OneGate.Backend.Core.Assets.Database.Models;
using OneGate.Backend.Core.Base.Database.Repository;

namespace OneGate.Backend.Core.Assets.Database.Repository
{
    public class LayerRepository : GenericRepository<Layer>, ILayerRepository
    {
        private readonly DatabaseContext _db;

        public LayerRepository(DatabaseContext db) : base(db, db.Layers)
        {
            _db = db;
        }
    }
}