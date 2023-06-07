using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Repositorys
{
    public class PhancongRepository : IPhanCongRepository
    {
        private readonly UehDbContext _context;

        public PhancongRepository(UehDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreatePhanCong(PhanCong Phancong)
        {
            var PhanCongkhoa = await _context.Phancongs.Where(a => a.mssv == Phancong.mssv).FirstOrDefaultAsync();

            if (PhanCongkhoa == null)
                _context.Add(Phancong);

            return await Save();
        }

        public async Task<bool> DeletePhanCong(PhanCong PhanCong)
        {
            _context.Remove(PhanCong);
            return await Save();
        }


        public async Task<PhanCong> GetPhanCong(string mssv)
        {
            return await _context.Phancongs.Where(s => s.mssv == mssv).FirstOrDefaultAsync();
        }



        public async Task<ICollection<PhanCong>> GetPhanCongs()
        {
            return await _context.Phancongs.OrderBy(s => s.mssv).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> PhanCongExists(string mssv)
        {
            return await _context.Phancongs.AnyAsync(s => s.mssv == mssv);
        }

        public async Task<bool> UpdatePhanCong(PhanCong PhanCong)
        {
            bool PhanCongExists = await _context.Phancongs.AnyAsync(s => s.mssv == PhanCong.mssv);

            if (PhanCongExists != false)
                _context.Update(PhanCong);
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

                        // Lặp qua các dòng trong tệp Excel và xử lý dữ liệu
                        // Bắt đầu từ dòng thứ 2 (loại bỏ header)
                        for (int row = 2; row <= rowCount; row++)
                        {

                            var phancong = new PhanCong
                            {
                                Id = Guid.NewGuid(),
                                mssv = worksheet.Cells[row, 2].Value?.ToString(),
                                magv = worksheet.Cells[row, 3].Value?.ToString(),
                                macn = worksheet.Cells[row, 4].Value?.ToString(),
                                maloai = worksheet.Cells[row, 5].Value?.ToString(),
                                madot = worksheet.Cells[row, 6].Value?.ToString(),
                            };

                            await _context.Phancongs.AddRangeAsync(phancong);
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
