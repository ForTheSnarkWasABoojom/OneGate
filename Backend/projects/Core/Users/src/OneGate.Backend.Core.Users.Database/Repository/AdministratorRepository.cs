using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Users.Database.Models;

namespace OneGate.Backend.Core.Users.Database.Repository
{
    public class AdministratorRepository : GenericRepository<Administrator>, IAdministratorRepository
    {
        private readonly DatabaseContext _db;

        public AdministratorRepository(DatabaseContext db) : base(db, db.Administrators)
        {
            _db = db;
        }
    }
}