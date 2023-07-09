using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Migrations;

namespace Ueh.BackendApi.Repositorys
{
    public class PhancongRepository : IPhancongRepository
    {
        private readonly UehDbContext _context;

        public PhancongRepository(UehDbContext context)
        {
            _context = context;
        }

        public async Task<bool> KiemTraMaloai(string mssv)
        {
            bool hasHKDN = await _context.Phancongs
                .AnyAsync(p => p.mssv == mssv && p.maloai == "HKDN");

            return hasHKDN;
        }

        public async Task<bool> CreatePhancong(Phancong phancong)
        {
            var Phancongkhoa = await _context.Phancongs.Where(a => a.mssv == phancong.mssv).FirstOrDefaultAsync();
            var ketqua = new Ketqua
            {
                mapc = phancong.Id,
            };
            var chitiet = new Chitiet
            {
                mapc = phancong.Id,
            };


            if (Phancongkhoa == null)
            {
                _context.Add(phancong);
                _context.Add(ketqua);
                _context.Add(chitiet);
            }




            return await Save();
        }

        public async Task<bool> DeletePhancong(Phancong phancong)
        {
            phancong.status = "false";
            _context.Update(phancong);
            return await Save();
        }


        public async Task<Phancong> GetPhancong(string mssv)
        {
            return await _context.Phancongs.Where(s => s.mssv == mssv).FirstOrDefaultAsync();
        }



        public async Task<ICollection<Phancong>> GetPhancongs()
        {
            return await _context.Phancongs.Where(s => s.status == "true").OrderBy(s => s.mssv).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> PhancongExists(string mssv)
        {
            return await _context.Phancongs.AnyAsync(s => s.mssv == mssv && s.status == "true");
        }


        public async Task<bool> UpdatePhancong(Phancong Phancong)
        {
            bool PhancongExists = await _context.Phancongs.AnyAsync(s => s.mssv == Phancong.mssv && s.status == "true");

            if (PhancongExists != false)
                _context.Update(Phancong);
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
                            var mssv = worksheet.Cells[row, 1].Value?.ToString();
                            var existing = await _context.Phancongs.FirstOrDefaultAsync(s => s.mssv == mssv && s.status == "true");

                            if (existing != null)
                            {
                                existing.magv = worksheet.Cells[row, 2].Value?.ToString();
                                existing.maloai = worksheet.Cells[row, 3].Value?.ToString();
                                existing.madot = worksheet.Cells[row, 4].Value?.ToString();
                            }

                            var phanconglist = new Phancong
                            {
                                Id = Guid.NewGuid(),
                                mssv = mssv,
                                magv = worksheet.Cells[row, 2].Value?.ToString(),
                                maloai = worksheet.Cells[row, 3].Value?.ToString(),
                                madot = worksheet.Cells[row, 4].Value?.ToString(),
                            };



                            var ketqua = new Ketqua
                            {
                                mapc = phanconglist.Id,
                            };
                            var chitiet = new Chitiet
                            {
                                mapc = phanconglist.Id,
                            };

                            _context.Add(ketqua);
                            _context.Add(chitiet);

                            await _context.Phancongs.AddAsync(phanconglist);
                        }

                        return await Save();
                    }
                }

            }
            return false;
        }

        public async Task<byte[]> ExportToExcel(string madot, string makhoa)
        {
            var Phancongs = await _context.Phancongs
                .Include(d => d.sinhvien)
                .Include(d => d.giangvien)
                .OrderBy(d => d.giangvien.tengv)
                .ToListAsync();

            // Tạo một package Excel
            using (var package = new ExcelPackage())
            {
                // Tạo một worksheet trong package
                var worksheet = package.Workbook.Worksheets.Add("Phancong");

                // Đặt tiêu đề cho các cột
                worksheet.Cells["A1"].Value = "STT";
                worksheet.Cells["B1"].Value = "MSSV";
                worksheet.Cells["C1"].Value = "Lớp Sinh Viên";
                worksheet.Cells["D1"].Value = "Họ";
                worksheet.Cells["E1"].Value = "Tên";
                worksheet.Cells["F1"].Value = "Giáo Viên Hướng Dẫn";

                // Ghi dữ liệu vào worksheet
                int rowIndex = 2;
                int count = 0;
                foreach (var Phancong in Phancongs)
                {
                    if (Phancong.status != "true")
                    {
                        continue; // Bỏ qua bản ghi không có status bằng "true"
                    }
                    worksheet.Cells[$"A{rowIndex}"].Value = count++;
                    worksheet.Cells[$"B{rowIndex}"].Value = Phancong.mssv;
                    worksheet.Cells[$"C{rowIndex}"].Value = Phancong.sinhvien?.thuoclop;
                    worksheet.Cells[$"D{rowIndex}"].Value = Phancong.sinhvien?.ho;
                    worksheet.Cells[$"E{rowIndex}"].Value = Phancong.sinhvien?.ten;
                    worksheet.Cells[$"F{rowIndex}"].Value = Phancong.giangvien?.tengv;

                    rowIndex++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.Cells.AutoFitColumns();

                // Xuất file Excel
                var content = package.GetAsByteArray();
                return content;
            }
        }

        public async Task<ICollection<Phancong>> SearchByTenSinhVien(string tenSinhVien)
        {
            var result = await _context.Phancongs
                        .Include(p => p.sinhvien)
                        .Where(p => p.sinhvien.ten.Contains(tenSinhVien))
                        .ToListAsync();

            return result;
        }

        public async Task<ICollection<Phancong>> GetPhanCongByMaGV(string magv)
        {

            var phanconglist = await _context.Phancongs
                .Where(p => p.magv == magv)
                .ToListAsync();
            return phanconglist;
        }


    }
}
