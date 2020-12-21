using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Core.Records.Database.Models;

namespace OneGate.Backend.Core.Records.Database.Repository
{
    public class LayoutRepository : ILayoutRepository
    {
        private readonly DatabaseContext _db;

        public LayoutRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(Layout model)
        {
            var layout = await _db.Layouts.AddAsync(model);
            await _db.SaveChangesAsync();

            return layout.Entity.Id;
        }

        public async Task<IEnumerable<Layout>> FilterAsync(int? id, string name, int shift, int count)
        {
            var layoutsQuery = _db.Layouts.AsQueryable();

            if (id != null)
                layoutsQuery = layoutsQuery.Where(x => x.Id == id);

            if (name != null)
                layoutsQuery = layoutsQuery.Where(x => x.Name == name);

            return await layoutsQuery.Skip(shift).Take(count).ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _db.Layouts.RemoveRange(_db.Layouts.Where(x => x.Id == id));
            await _db.SaveChangesAsync();
        }
    }
}