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
            return await _context.Sinhviens.Where(s => s.mssv == mssv).FirstOrDefaultAsync();
        }

        public async Task<Sinhvien> GetSinhvienName(string name)
        {
            return await _context.Sinhviens.Where(s => s.hoten == name).FirstOrDefaultAsync();

        }

        public async Task<ICollection<Sinhvien>> GetSinhviens()
        {
            return await _context.Sinhviens.OrderBy(s => s.mssv).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> SinhvienExists(string mssv)
        {
            return await _context.Sinhviens.AnyAsync(s => s.mssv == mssv);
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
                            var sinhvien = new Sinhvien
                            {
                                mssv = worksheet.Cells[row, 2].Value?.ToString(),
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


        public async Task<byte[]> ExportToExcel()
        {
            var PhanCongs = await _context.Phancongs
                .Include(d => d.Sinhvien)
                .Include(d => d.Giangvien)
                .ToListAsync();

            // Tạo một package Excel
            using (var package = new ExcelPackage())
            {
                // Tạo một worksheet trong package
                var worksheet = package.Workbook.Worksheets.Add("PhanCong");

                // Đặt tiêu đề cho các cột
                worksheet.Cells["A1"].Value = "STT";
                worksheet.Cells["B1"].Value = "MSSV";
                worksheet.Cells["C1"].Value = "Lớp Sinh Viên";
                worksheet.Cells["D1"].Value = "Họ Tên Sinh Viên";
                worksheet.Cells["E1"].Value = "Ngày Sinh";
                worksheet.Cells["F1"].Value = "Giáo Viên Hướng Dẫn";

                // Ghi dữ liệu vào worksheet
                int rowIndex = 2;
                int count = 0;
                foreach (var PhanCong in PhanCongs)
                {
                    worksheet.Cells[$"A{rowIndex}"].Value = count++;
                    worksheet.Cells[$"B{rowIndex}"].Value = PhanCong.mssv;
                    worksheet.Cells[$"C{rowIndex}"].Value = PhanCong.Sinhvien?.tenlop;
                    worksheet.Cells[$"D{rowIndex}"].Value = PhanCong.Sinhvien?.hoten;
                    worksheet.Cells[$"E{rowIndex}"].Value = PhanCong.Sinhvien?.ngaysinh;
                    worksheet.Cells[$"F{rowIndex}"].Value = PhanCong.Giangvien?.tengv;

                    rowIndex++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.Cells.AutoFitColumns();

                // Xuất file Excel
                var content = package.GetAsByteArray();
                return content;
            }
        }
    }
}
