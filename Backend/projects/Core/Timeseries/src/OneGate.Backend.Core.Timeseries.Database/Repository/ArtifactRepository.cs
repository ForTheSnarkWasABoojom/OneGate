using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database.Repository
{
    public class ArtifactRepository : GenericRepository<Artifact>, IArtifactRepository
    {
        private readonly DatabaseContext _db;

        public ArtifactRepository(DatabaseContext db) : base(db, db.Artifacts)
        {
            _db = db;
        }
    }
}