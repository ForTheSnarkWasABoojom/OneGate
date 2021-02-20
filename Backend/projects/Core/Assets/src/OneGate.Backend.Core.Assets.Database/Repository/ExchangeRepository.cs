using OneGate.Backend.Core.Assets.Database.Models;
using OneGate.Backend.Core.Shared.Database.Repository;

namespace OneGate.Backend.Core.Assets.Database.Repository
{
    public class ExchangeRepository : GenericRepository<Exchange>, IExchangeRepository
    {
        private readonly DatabaseContext _db;

        public ExchangeRepository(DatabaseContext db) : base(db, db.Exchanges)
        {
            _db = db;
        }
    }
}