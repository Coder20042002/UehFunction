using Microsoft.EntityFrameworkCore;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Repositorys
{
    public class ChitietRepository : IChitietRepository
    {
        private readonly UehDbContext _context;

        public ChitietRepository(UehDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Chitiet>> GetChitiet()
        {
            return await _context.Chitiets.OrderBy(s => s.mssv).ToListAsync();
        }

        public async Task<Chitiet> GetChitiet(string mssv)
        {
            return await _context.Chitiets.Where(s => s.mssv == mssv).FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> ChitietExists(string mssv)
        {
            return await _context.Chitiets.AnyAsync(s => s.mssv == mssv);
        }

        public Task<bool> UpdateChitiet(Chitiet Chitiet)
        {
            _context.Update(Chitiet);
            return Save();
        }
    }
}

