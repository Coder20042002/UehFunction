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

        public async Task<ICollection<Giangvien>> GetKhoaByGiangviens(string makhoa)
        {
            return await _context.Giangviens.Where(k => k.makhoa == makhoa).ToListAsync();

        }

        public async Task<ICollection<Sinhvien>> GetKhoaBySinhviens(string madot, string makhoa)
        {
            return await _context.Sinhviens
                .Where(sk => sk.makhoa == makhoa && sk.madot == madot && sk.status == "true")
                .ToListAsync();
        }

    }
}
