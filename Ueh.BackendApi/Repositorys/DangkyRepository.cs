using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Repositorys
{
    public class DangkyRepository : IDangkyRepository
    {
        private readonly UehDbContext _context;

        public DangkyRepository(UehDbContext context)
        {
            _context = context;
        }
        public async Task<List<Dangky>> GetSinhVienByGiaoVien(string madot, string makhoa, string maGiaoVien)
        {
            var sinhVienList = await _context.Dangkys
                .Where(dk => dk.magv == maGiaoVien && dk.makhoa == makhoa && dk.madot == madot)
                .ToListAsync();

            return sinhVienList;
        }

        public async Task<bool> CreateDangky(Dangky Dangky)
        {
            var Dangkykhoa = await _context.Dangkys.Where(a => a.mssv == Dangky.mssv).FirstOrDefaultAsync();

            if (Dangkykhoa == null)
                _context.Add(Dangky);
            return await Save();
        }

        public async Task<bool> DeleteDangky(Dangky dangky)
        {
            dangky.status = "false";
            _context.Update(dangky);
            return await Save();
        }


        public async Task<Dangky> GetDangky(string mssv)
        {
            return await _context.Dangkys.Where(s => s.mssv == mssv && s.status == "true").FirstOrDefaultAsync();
        }



        public async Task<ICollection<Dangky>> GetDangkys()
        {
            return await _context.Dangkys.Where(s => s.status == "true").OrderBy(s => s.mssv).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> DangkyExists(string mssv)
        {
            return await _context.Dangkys.AnyAsync(s => s.mssv == mssv && s.status == "true");
        }

        public async Task<bool> UpdateDangky(Dangky Dangky)
        {
            bool dangkyExists = await _context.Dangkys.AnyAsync(s => s.mssv == Dangky.mssv && s.status == "true");

            if (dangkyExists != false)
                _context.Update(Dangky);
            return await Save();
        }

        public async Task<bool> ImportExcelFile(IFormFile formFile, string madot, string makhoa, string magv)
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


                        for (int row = 1; row <= rowCount; row++)
                        {
                            var mssv = worksheet.Cells[row, 1].Value?.ToString();
                            bool existing = await _context.Dangkys.AnyAsync(s => s.mssv == mssv && s.status == "true");

                            if (existing != false)
                            {
                                continue;
                            }
                            var dangky = new Dangky
                            {
                                mssv = mssv,
                                ho = worksheet.Cells[row, 2].Value?.ToString(),
                                ten = worksheet.Cells[row, 3].Value?.ToString(),
                                lop = worksheet.Cells[row, 4].Value?.ToString(),
                                email = worksheet.Cells[row, 5].Value?.ToString(),
                                madot = madot,
                                magv = magv,
                                makhoa = makhoa
                            };

                            await _context.Dangkys.AddAsync(dangky);
                        }

                        return await Save();
                    }
                }

            }
            return false;
        }

        public async Task<byte[]> ExportToExcel()
        {
            var dangkys = await _context.Dangkys
                .Include(d => d.giangvien)
                .ToListAsync();

            // Tạo một package Excel
            using (var package = new ExcelPackage())
            {
                // Tạo một worksheet trong package
                var worksheet = package.Workbook.Worksheets.Add("DangKy");

                // Đặt tiêu đề cho các cột
                worksheet.Cells["A1"].Value = "STT";
                worksheet.Cells["B1"].Value = "MSSV";
                worksheet.Cells["C1"].Value = "Họ tên sinh viên";
                worksheet.Cells["D1"].Value = "MaGV";
                worksheet.Cells["E1"].Value = "Giáo viên hướng dẫn";
                worksheet.Cells["F1"].Value = "MaLoai";
                worksheet.Cells["G1"].Value = "Tên Loại";

                // Ghi dữ liệu vào worksheet
                int rowIndex = 2;
                int count = 0;
                foreach (var dangky in dangkys)
                {
                    if (dangky.status != "true")
                    {
                        continue; // Bỏ qua bản ghi không có status bằng "true"
                    }
                    worksheet.Cells[$"A{rowIndex}"].Value = count++;
                    worksheet.Cells[$"B{rowIndex}"].Value = dangky.mssv;
                    worksheet.Cells[$"C{rowIndex}"].Value = dangky.ho + " " + dangky.ten;
                    worksheet.Cells[$"D{rowIndex}"].Value = dangky.magv;
                    worksheet.Cells[$"E{rowIndex}"].Value = dangky.giangvien?.tengv;

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

