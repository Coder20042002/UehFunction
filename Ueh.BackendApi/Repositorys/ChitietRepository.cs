using Microsoft.EntityFrameworkCore;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Request;

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

        public async Task<ChitietRequest> GetChitiet(string mssv)
        {
            var phanCongIds = await _context.Phancongs.FirstOrDefaultAsync(ct => ct.status == "true" && ct.mssv == mssv);
            var chitiet = await _context.Chitiets.FirstOrDefaultAsync(kq => kq.mapc == phanCongIds.Id);
            var user = await _context.Users.FirstOrDefaultAsync(c => c.userId == mssv);
            
            var request = new ChitietRequest
            {
                emailsv = user.email,
                sdtsv = user.sdt,
                chucvu = chitiet.chucvu,
                huongdan = chitiet.huongdan,
                emailhd = chitiet.emailhd,
                sdthd = chitiet.sdthd,
                tencty = chitiet.tencty,
                vitri = chitiet.vitri,
                website = chitiet.website,
                tendetai = chitiet.tendetai

            };
            return request;
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

        public async Task<bool> UpdateChitiet(ChitietRequest updatechitiet, string mssv)
        {

            var phancong = await _context.Phancongs.FirstOrDefaultAsync(p => p.mssv == mssv);

            if (phancong == null)
            {
                return false;
            }

            var chitiet = await _context.Chitiets.FirstOrDefaultAsync(k => k.mapc == phancong.Id);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.userId == mssv);


            if (chitiet == null || user == null)
            {
                return false;
            }

            user.email = updatechitiet.emailsv;
            user.sdt = updatechitiet.sdtsv;

            chitiet.tencty = updatechitiet.tencty;
            chitiet.vitri = updatechitiet.vitri;
            chitiet.website = updatechitiet.website;
            chitiet.huongdan = updatechitiet.huongdan;
            chitiet.chucvu = updatechitiet.chucvu;
            chitiet.emailhd = updatechitiet.emailhd;
            chitiet.sdthd = updatechitiet.sdthd;
            chitiet.tendetai = updatechitiet.tendetai;
            chitiet.status = "true";

            _context.Chitiets.Update(chitiet);
            _context.Users.Update(user);

            return await Save();
        }
    }
}

