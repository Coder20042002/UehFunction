using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.Repositorys
{
    public class LichsuRepository : ILichsuRepository
    {
        private readonly UehDbContext _context;

        public LichsuRepository(UehDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateLichsu(LichsuRequest lichsurequest, string mssv)
        {
            var phancong = await _context.Phancongs.FirstOrDefaultAsync(p => p.mssv == mssv);
            if (phancong != null)
            {
                DateTime ngay;
                if (DateTime.TryParseExact(lichsurequest.ngay, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out ngay))
                {
                    var lichsu = new Lichsu
                    {
                        Id = phancong.Id,
                        ngay = ngay,
                        noidung = lichsurequest.noidung
                    };
                    _context.Add(lichsu);
                }
                else
                {
                    // Xử lý khi không thể chuyển đổi chuỗi thành DateTime

                }
            }
            return await Save();
        }

        public Task<bool> DeleteLichsu(Lichsu lichsu)
        {
            _context.Remove(lichsu);
            return Save();
        }

        public Task<Lichsu> GetLichsu(Guid mapc)
        {
            return _context.Lichsus.Where(r => r.Id == mapc && r.phancong.status == "true").FirstOrDefaultAsync();
        }

        public async Task<ICollection<Lichsu>> GetLichSuByMssv(string madot, string mssv)
        {
            var phanCong = await _context.Phancongs.FirstOrDefaultAsync(pc => pc.mssv == mssv && pc.madot == madot && pc.status == "true");

            var lichSu = await _context.Lichsus.Where(ls => ls.Id == phanCong.Id).ToListAsync();
            return lichSu;

        }

        public async Task<ICollection<Lichsu>> GetLichsus()
        {
            return await _context.Lichsus.Where(r => r.phancong.status == "true").ToListAsync();
        }

        public async Task<ICollection<Lichsu>> GetLichsusOfASinhvien(Guid mapc)
        {
            return await _context.Lichsus.Where(r => r.Id == mapc && r.phancong.status == "true").ToListAsync();
        }



        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateLichsu(Lichsu lichsu)
        {
            ;
            var kiemtra = await _context.Lichsus.FirstOrDefaultAsync(s => s.Id == lichsu.Id && s.ngay == lichsu.ngay);

            if (kiemtra != null)
            {
                kiemtra.noidung = lichsu.noidung;
                _context.Update(kiemtra);
            }
            return await Save();
        }


    }
}
