using Microsoft.EntityFrameworkCore;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Repositorys
{
    public class LichsuRepository : ILichsuRepository
    {
        private readonly UehDbContext _context;

        public LichsuRepository(UehDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateLichsu(Lichsu lichsu)
        {
            _context.Add(lichsu);
            return await Save();
        }

        public Task<bool> DeleteLichsu(Lichsu lichsu)
        {
            _context.Remove(lichsu);
            return Save();
        }

        public Task<Lichsu> GetLichsu(Guid mapc)
        {
            return _context.Lichsus.Where(r => r.mapc == mapc && r.phancong.status == "true").FirstOrDefaultAsync();
        }

        public async Task<ICollection<Lichsu>> GetLichsus()
        {
            return await _context.Lichsus.Where(r => r.phancong.status == "true").ToListAsync();
        }

        public async Task<ICollection<Lichsu>> GetLichsusOfASinhvien(Guid mapc)
        {
            return await _context.Lichsus.Where(r => r.mapc == mapc && r.phancong.status == "true").ToListAsync();
        }

        public async Task<bool> LichsuExists(Guid mapc, DateTime dateTime)
        {
            return await _context.Lichsus.AnyAsync(r => r.mapc == mapc && r.ngay == dateTime && r.phancong.status == "true");
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public Task<bool> UpdateLichsu(Lichsu lichsu)
        {
            _context.Update(lichsu);
            return Save();
        }


    }
}
