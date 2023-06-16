using Microsoft.EntityFrameworkCore;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Repositorys
{
    public class KetquaRepository : IKetquaRepository
    {
        private readonly UehDbContext _context;

        public KetquaRepository(UehDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Ketqua>> GetScores()
        {
            return await _context.Ketquas.OrderBy(s => s.mssv).ToListAsync();
        }

        public async Task<Ketqua> GetScores(string mssv)
        {
            return await _context.Ketquas.Where(s => s.mssv == mssv).FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> ScoresExists(string mssv)
        {
            return await _context.Ketquas.AnyAsync(s => s.mssv == mssv);
        }

        public Task<bool> UpdateScores(Ketqua ketqua)
        {
            _context.Update(ketqua);
            return Save();
        }
    }
}
