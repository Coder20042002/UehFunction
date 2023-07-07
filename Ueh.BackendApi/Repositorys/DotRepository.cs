using Microsoft.EntityFrameworkCore;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Repositorys
{
    public class DotRepository : IDotRepository
    {
        private readonly UehDbContext _context;

        public DotRepository(UehDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateDot(Dot dot)
        {
            _context.Add(dot);
            return await Save();
        }

        public async Task<bool> DeleteDot(Dot dot)
        {
            _context.Remove(dot);
            return await Save();
        }

        public async Task<bool> DotExists(string id)
        {
            return await _context.Dots.AnyAsync(c => c.madot == id);
        }

        public async Task<ICollection<Dot>> GetAllDot()
        {
            return await _context.Dots.ToListAsync();
        }



        public async Task<Dot> GetDot()
        {
            return await _context.Dots.Where(e => e.status == "true").FirstOrDefaultAsync();

        }



        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateDot(Dot dot)
        {
            _context.Update(dot);
            return await Save();
        }
    }
}
