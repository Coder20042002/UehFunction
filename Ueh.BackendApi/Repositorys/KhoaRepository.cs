using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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

        public async Task<ICollection<Khoa>> GetListKhoa()
        {
            return await _context.Khoas.ToListAsync();
        }

        public async Task<Khoa> GetKhoaById(string makhoa)
        {
            return await _context.Khoas.Where(s => s.makhoa == makhoa).FirstOrDefaultAsync();
        }


        public async Task<bool> CreateKhoa(Khoa Khoa)
        {
            var khoa = await _context.Khoas.Where(a => a.makhoa == Khoa.makhoa).FirstOrDefaultAsync();

            if (khoa == null)
                _context.Add(Khoa);

            return await Save();
        }

        public async Task<bool> DeleteKhoa(Khoa Khoa)
        {
            _context.Remove(Khoa);
            return await Save();
        }


        public async Task<bool> UpdateKhoa(Khoa khoa)
        {
            bool KhoaExists = await _context.Khoas.AnyAsync(s => s.makhoa == khoa.makhoa);

            if (KhoaExists != false)
                _context.Update(khoa);
            return await Save();
        }

        public async Task<bool> ImportExcelFile(IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    formFile.CopyTo(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;


                        for (int row = 2; row <= rowCount; row++)
                        {
                            var makhoa = worksheet.Cells[row, 1].Value?.ToString();
                            bool existing = await _context.Khoas.AnyAsync(s => s.makhoa == makhoa);

                            if (existing != false)
                            {
                                continue;
                            }
                            var khoa = new Khoa
                            {
                                makhoa = worksheet.Cells[row, 1].Value?.ToString(),
                                tenkhoa = worksheet.Cells[row, 2].Value?.ToString(),
                            };

                            await _context.Khoas.AddAsync(khoa);
                        }

                        return await Save();
                    }
                }

            }
            return false;
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> KhoaExists(string makhoa)
        {
            return await _context.Khoas.AnyAsync(s => s.makhoa == makhoa);
        }
    }
}
