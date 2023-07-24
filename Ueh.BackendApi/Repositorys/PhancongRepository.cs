using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ServiceStack;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.Repositorys
{
    public class PhancongRepository : IPhancongRepository
    {
        private readonly UehDbContext _context;

        public PhancongRepository(UehDbContext context)
        {
            _context = context;
        }

        public async Task<bool> KiemTraMaloai(string madot, string mssv)
        {
            bool hasHKDN = await _context.Phancongs
                .AnyAsync(p => p.mssv == mssv && p.maloai == "HKDN" && p.madot == madot);

            return hasHKDN;
        }

        public async Task<bool> CreatePhancong(PhancongRequest phancongrequest)
        {
            bool sinvien = await _context.Sinhviens.AnyAsync(s => s.mssv == phancongrequest.mssv && s.status == "true" && s.madot == phancongrequest.madot);
            if (sinvien)
            {
                bool kiemtra = await _context.Phancongs.AnyAsync(s => s.mssv == phancongrequest.mssv && s.status == "true" && s.madot == phancongrequest.madot);
                if (!kiemtra)
                {
                    var phancong = new Phancong
                    {
                        Id = Guid.NewGuid(),
                        mssv = phancongrequest.mssv,
                        magv = phancongrequest.magv,
                        maloai = phancongrequest.maloai,
                        madot = phancongrequest.madot,
                        status = "true"
                    };

                    var ketqua = new Ketqua
                    {
                        mapc = phancong.Id,
                    };
                    var chitiet = new Chitiet
                    {
                        mapc = phancong.Id,
                    };


                    _context.Add(phancong);
                    _context.Add(ketqua);
                    _context.Add(chitiet);

                }
            }


            return await Save();
        }

        public async Task<bool> DeletePhancong(string madot, string mssv)
        {
            var kiemtra = await _context.Phancongs.FirstOrDefaultAsync(s => s.mssv == mssv && s.status == "true" && s.madot == madot);

            if (kiemtra != null)
            {
                kiemtra.status = "false";
                _context.Update(kiemtra);
            }

            return await Save();
        }


        public async Task<Phancong> GetPhancong(string mssv)
        {
            return await _context.Phancongs.Where(s => s.mssv == mssv).FirstOrDefaultAsync();
        }



        public async Task<ICollection<Phancong>> GetPhancongKhoas(string madot, string makhoa)
        {
            var phancongs = await _context.Phancongs
                .Include(p => p.sinhvien)
                .Where(p => p.madot == madot && p.status == "true" && p.sinhvien.makhoa == makhoa)
                .ToListAsync();

            return phancongs;
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


        public async Task<bool> UpdatePhancong(string madot, string mssv, string magv)
        {
            var kiemtra = await _context.Phancongs.FirstOrDefaultAsync(s => s.mssv == mssv && s.status == "true" && s.madot == madot);

            if (kiemtra != null)
            {
                kiemtra.magv = magv;
                _context.Update(kiemtra);
            }

            return await Save();
        }

        public async Task<bool> UpdateLoaiHinhThucTap(string madot, string mssv, string maloai)
        {
            var kiemtra = await _context.Phancongs.FirstOrDefaultAsync(s => s.mssv == mssv && s.status == "true" && s.madot == madot);

            if (kiemtra != null)
            {
                kiemtra.maloai = maloai;
                _context.Update(kiemtra);
            }

            return await Save();
        }

        public async Task<bool> ImportExcelFile(IFormFile formFile, string madot)
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

                        // Trước khi bắt đầu vòng lặp, tạo một từ điển lưu trữ maloai tương ứng với mssv
                        var maloaiDict = await _context.Sinhviens
                                        .Where(sv => sv.madot == madot) // Lọc sinh viên dựa vào đợt (madot)
                                        .ToDictionaryAsync(sv => sv.mssv, sv => sv.malop);
                        // Lặp qua các dòng trong tệp Excel và xử lý dữ liệu
                        // Bắt đầu từ dòng thứ 2 (loại bỏ header)
                        for (int row = 2; row <= rowCount; row++)
                        {
                            var mssv = worksheet.Cells[row, 1].Value?.ToString();
                            var magv = worksheet.Cells[row, 2].Value?.ToString();

                            bool sinvien = await _context.Sinhviens.AnyAsync(s => s.mssv == mssv && s.status == "true" && s.madot == madot);
                            if (sinvien)
                            {
                                bool kiemtra = await _context.Phancongs.AnyAsync(s => s.mssv == mssv && s.status == "true" && s.madot == madot);
                                if (kiemtra != false)
                                {
                                    continue;
                                }
                                else
                                {
                                    var phanconglist = new Phancong
                                    {
                                        Id = Guid.NewGuid(),
                                        mssv = mssv,
                                        magv = magv,
                                        maloai = maloaiDict.GetValueOrDefault(mssv).Substring(1, 4), // Lấy maloai từ từ điển,
                                        madot = madot,
                                        status = "true"
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
                            }



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
                .Where(d => d.madot == madot && d.sinhvien.makhoa == makhoa)
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
