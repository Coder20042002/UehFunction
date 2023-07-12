using Microsoft.EntityFrameworkCore;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Repositorys
{
    public class KhoaRepository : IKhoaRepository
    {
        private readonly UehDbContext _context;

        public KhoaRepository(UehDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<GiangvienKhoa>> GetKhoaByGiangviens(string makhoa)
        {
            return await _context.GiangvienKhoas.Where(k => k.makhoa == makhoa).ToListAsync();

        }

        public async Task<ICollection<SinhvienKhoa>> GetKhoaBySinhviens(string madot, string makhoa)
        {
            return await _context.SinhvienKhoas
                .Include(sk => sk.sinhvien)
                .Where(sk => sk.makhoa == makhoa && sk.sinhvien.madot == madot && sk.sinhvien.status == "true")
                .ToListAsync();
        }

    }
}
