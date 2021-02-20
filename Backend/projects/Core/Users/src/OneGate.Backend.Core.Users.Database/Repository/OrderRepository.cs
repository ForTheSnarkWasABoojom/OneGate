using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Database.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DatabaseContext _db;

        public OrderRepository(DatabaseContext db) : base(db, db.Orders)
        {
            _db = db;
        }
    }
}