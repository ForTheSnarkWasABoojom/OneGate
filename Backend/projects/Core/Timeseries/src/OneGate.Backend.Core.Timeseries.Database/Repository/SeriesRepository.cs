using OneGate.Backend.Core.Base.Database.Repository;
using OneGate.Backend.Core.Timeseries.Database.Models;

namespace OneGate.Backend.Core.Timeseries.Database.Repository
{
    public class SeriesRepository : GenericRepository<Series>, ISeriesRepository
    {
        private readonly DatabaseContext _db;

        public SeriesRepository(DatabaseContext db) : base(db, db.Series)
        {
            _db = db;
        }
    }
}