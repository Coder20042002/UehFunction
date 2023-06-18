using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ServiceStack;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Repositorys
{
    public class SinhvienRepository : ISinhvienRepository
    {
        private readonly UehDbContext _context;

        public SinhvienRepository(UehDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateSinhvien(string madot, string makhoa, Sinhvien sinhvien)
        {
            var sinhviendotEntity = await _context.Dots.Where(a => a.madot == madot).FirstOrDefaultAsync();
            var sinhvienkhoaEntity = await _context.Khoas.Where(a => a.makhoa == makhoa).FirstOrDefaultAsync();

            var sinhviendot = new SinhvienDot()
            {
                dot = sinhviendotEntity,
                sinhvien = sinhvien,
            };

            var sinhvienkhoa = new SinhvienKhoa()
            {
                khoa = sinhvienkhoaEntity,
                sinhvien = sinhvien,
            };

            _context.Add(sinhviendot);
            _context.Add(sinhvienkhoa);

            _context.Add(sinhvien);

            return await Save();
        }

        public Task<bool> DeleteSinhvien(Sinhvien sinhvien)
        {
            sinhvien.status = "false";
            _context.Update(sinhvien);
            return Save();
        }

        public async Task<Sinhvien> GetSinhvienTrimToUpper(SinhvienDto sinhvienCreate)
        {
            var getsv = await GetSinhviens();
            return getsv.Where(c => c.mssv.Trim().ToUpper() == sinhvienCreate.hoten.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public async Task<Sinhvien> GetSinhvien(string mssv)
        {
            return await _context.Sinhviens.Where(s => s.mssv == mssv && s.status == "true").FirstOrDefaultAsync();
        }

        public async Task<Sinhvien> GetSinhvienName(string name)
        {
            return await _context.Sinhviens.Where(s => s.hoten == name && s.status == "true").FirstOrDefaultAsync();

        }

        public async Task<ICollection<Sinhvien>> GetSinhviens()
        {
            return await _context.Sinhviens.Where(s => s.status == "true").OrderBy(s => s.mssv).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> SinhvienExists(string mssv)
        {
            return await _context.Sinhviens.AnyAsync(s => s.mssv == mssv && s.status == "true");
        }

        public Task<bool> UpdateSinhvien(string madot, string makhoa, Sinhvien sinhvien)
        {
            _context.Update(sinhvien);
            return Save();
        }
        public async Task<bool> ImportExcelFile(string madot, string makhoa, IFormFile formFile)
        {
            var sinhviendotEntity = await _context.Dots.Where(a => a.madot == madot).FirstOrDefaultAsync();
            var sinhvienkhoaEntity = await _context.Khoas.Where(a => a.makhoa == makhoa).FirstOrDefaultAsync();

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
                            var mssv = worksheet.Cells[row, 2].Value?.ToString();
                            bool existing = await _context.Sinhviens.AnyAsync(s => s.mssv == mssv && s.status == "true"); ;

                            if (existing != false)
                            {
                                // Nếu MSSV đã tồn tại, bỏ qua sinh viên này và tiếp tục với dòng tiếp theo
                                continue;
                            }

                            var sinhvien = new Sinhvien
                            {
                                mssv = mssv,
                                hoten = worksheet.Cells[row, 3].Value?.ToString(),
                                email = worksheet.Cells[row, 4].Value?.ToString(),
                                tenlop = worksheet.Cells[row, 5].Value?.ToString(),
                                ngaysinh = worksheet.Cells[row, 6].Value?.ToString(),
                                sdt = worksheet.Cells[row, 7].Value?.ToString(),
                                HDT = worksheet.Cells[row, 8].Value?.ToString(),
                                status = "true"
                            };

                            var sinhviendot = new SinhvienDot
                            {
                                dot = sinhviendotEntity,
                                sinhvien = sinhvien
                            };

                            var sinhvienkhoa = new SinhvienKhoa()
                            {
                                khoa = sinhvienkhoaEntity,
                                sinhvien = sinhvien,
                            };

                            await _context.SinhvienDots.AddAsync(sinhviendot);
                            await _context.SinhvienKhoas.AddAsync(sinhvienkhoa);
                            await _context.Sinhviens.AddAsync(sinhvien);

                        }
                        return await Save();
                    }
                }
            }

            return false;
        }



    }
}
