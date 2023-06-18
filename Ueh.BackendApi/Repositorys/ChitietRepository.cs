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

        public async Task<ICollection<Chitiet>> GetChitietByMaGV(string magv)
        {
            var phanCongIds = await _context.Phancongs.Where(ct => ct.magv == magv && ct.status == "true").Select(ct => ct.Id).ToListAsync();
            var ketQuaList = await _context.Chitiets.Where(kq => phanCongIds.Contains(kq.mapc)).ToListAsync();


            return ketQuaList;
        }

        public async Task<ICollection<Chitiet>> GetChitiet()
        {
            var phanCongIds = await _context.Phancongs.Where(ct => ct.status == "true").Select(ct => ct.Id).ToListAsync();
            return await _context.Chitiets.Where(kq => phanCongIds.Contains(kq.mapc)).OrderBy(s => s.mssv).ToListAsync();
        }

        public async Task<Chitiet> GetChitiet(string mssv)
        {
            var phanCongIds = await _context.Phancongs.Where(ct => ct.status == "true").Select(ct => ct.Id).ToListAsync();
            return await _context.Chitiets.Where(kq => kq.mssv == mssv && phanCongIds.Contains(kq.mapc)).FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> ChitietExists(string mssv)
        {
            var phanCongIds = await _context.Phancongs.Where(ct => ct.status == "true").Select(ct => ct.Id).ToListAsync();
            return await _context.Chitiets.AnyAsync(s => s.mssv == mssv && phanCongIds.Contains(s.mapc));
        }

        public Task<bool> UpdateChitiet(Chitiet ketqua)
        {
            _context.Update(ketqua);
            return Save();
        }
    }
}

