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

        public async Task<ICollection<Chitiet>> GetChitiets()
        {
            var phanCongIds = await _context.Phancongs.Where(ct => ct.status == "true").Select(ct => ct.Id).ToListAsync();
            return await _context.Chitiets.Where(kq => phanCongIds.Contains(kq.mapc)).OrderBy(s => s.mapc).ToListAsync();
        }

        public async Task<Chitiet> GetChitiet(string mssv)
        {
            var phanCongIds = await _context.Phancongs.FirstOrDefaultAsync(ct => ct.status == "true" && ct.mssv == mssv);

            return await _context.Chitiets.FirstOrDefaultAsync(kq => kq.mapc == phanCongIds.Id);
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> ChitietExists(Guid mapc)
        {
            var phanCongIds = await _context.Phancongs.Where(ct => ct.status == "true").Select(ct => ct.Id).ToListAsync();
            return await _context.Chitiets.AnyAsync(s => s.mapc == mapc && phanCongIds.Contains(s.mapc));
        }

        public async Task<bool> UpdateChitiet(Chitiet updatechitiet, string mssv)
        {
            var phancong = await _context.Phancongs.FirstOrDefaultAsync(p => p.mssv == mssv);

            if (phancong == null)
            {
                return false;
            }

            var chitiet = await _context.Chitiets.FirstOrDefaultAsync(k => k.mapc == phancong.Id);

            if (chitiet == null)
            {
                return false;
            }

            chitiet.emailsv = updatechitiet.emailsv;
            chitiet.tencty = updatechitiet.tencty;
            chitiet.vitri = updatechitiet.vitri;
            chitiet.sdt = updatechitiet.sdt;
            chitiet.website = updatechitiet.website;
            chitiet.huongdan = updatechitiet.huongdan;
            chitiet.chucvu = updatechitiet.chucvu;
            chitiet.email = updatechitiet.email;
            chitiet.stdhd = updatechitiet.stdhd;
            chitiet.tendetai = updatechitiet.tendetai;
            chitiet.status = "true";
            _context.Chitiets.Update(chitiet);
            return await Save();
        }
    }
}

