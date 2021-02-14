using OneGate.Backend.Core.Base.Database.Repository;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Database.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly DatabaseContext _db;

        public AccountRepository(DatabaseContext db) : base(db, db.Accounts)
        {
            _db = db;
        }
    }
}