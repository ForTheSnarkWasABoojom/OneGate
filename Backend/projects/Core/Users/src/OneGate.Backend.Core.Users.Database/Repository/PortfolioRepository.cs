using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Database.Repository
{
    public class PortfolioRepository : GenericRepository<Portfolio>, IPortfolioRepository
    {
        private readonly DatabaseContext _db;

        public PortfolioRepository(DatabaseContext db) : base(db, db.Portfolios)
        {
            _db = db;
        }
    }
}