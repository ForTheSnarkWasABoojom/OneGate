using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Shared.Models.Layout;

namespace OneGate.Backend.Services.AssetService.Repository
{
    public class LayoutRepository : ILayoutRepository
    {
        private readonly DatabaseContext _db;

        public LayoutRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(CreateLayoutDto model)
        {
            var layout = await _db.Layouts.AddAsync(new Layout
            {
                Name = model.Name,
                Description = model.Description,
            });
            await _db.SaveChangesAsync();

            return layout.Entity.Id;
        }

        public async Task<LayoutDto> FindAsync(int id)
        {
            var layout = await _db.Layouts.FirstOrDefaultAsync(x => x.Id == id);
            return ConvertLayoutToDto(layout);
        }

        public async Task<IEnumerable<LayoutDto>> FilterAsync(LayoutFilterDto filter)
        {
            var layoutsQuery = _db.Layouts.AsQueryable();

            if (filter.Id != null)
                layoutsQuery = layoutsQuery.Where(x => x.Id == filter.Id);

            if (filter.Name != null)
                layoutsQuery = layoutsQuery.Where(x => x.Name == filter.Name);

            var layouts = await layoutsQuery.Skip(filter.Shift).Take(filter.Count).ToListAsync();

            return layouts.Select(ConvertLayoutToDto);
        }

        public async Task RemoveAsync(int id)
        {
            _db.Layouts.RemoveRange(_db.Layouts.Where(x => x.Id == id));
            await _db.SaveChangesAsync();
        }

        private static LayoutDto ConvertLayoutToDto(Layout layout)
        {
            return new LayoutDto
            {
                Id = layout.Id,
                Name = layout.Name,
                Description = layout.Description,
            };
        }
    }
}