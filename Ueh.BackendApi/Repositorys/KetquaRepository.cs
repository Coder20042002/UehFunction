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

        public async Task<ICollection<Ketqua>> GetKetQuaByMaGV(string magv)
        {
            var phanCongIds = await _context.Phancongs.Where(pc => pc.magv == magv && pc.status == "true").Select(pc => pc.Id).ToListAsync();
            var ketQuaList = await _context.Ketquas.Where(kq => phanCongIds.Contains(kq.mapc)).ToListAsync();


            return ketQuaList;
        }

        public async Task<ICollection<Ketqua>> GetScores()
        {
            var phanCongIds = await _context.Phancongs.Where(pc => pc.status == "true").Select(pc => pc.Id).ToListAsync();
            return await _context.Ketquas.Where(kq => phanCongIds.Contains(kq.mapc)).OrderBy(s => s.mssv).ToListAsync();
        }

        public async Task<Ketqua> GetScores(string mssv)
        {
            var phanCongIds = await _context.Phancongs.Where(pc => pc.status == "true").Select(pc => pc.Id).ToListAsync();
            return await _context.Ketquas.Where(kq => kq.mssv == mssv && phanCongIds.Contains(kq.mapc)).FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> ScoresExists(string mssv)
        {
            var phanCongIds = await _context.Phancongs.Where(pc => pc.status == "true").Select(pc => pc.Id).ToListAsync();
            return await _context.Ketquas.AnyAsync(kq => kq.mssv == mssv && phanCongIds.Contains(kq.mapc));
        }

        public Task<bool> UpdateScores(Ketqua ketqua)
        {
            _context.Update(ketqua);
            return Save();
        }
    }
}
