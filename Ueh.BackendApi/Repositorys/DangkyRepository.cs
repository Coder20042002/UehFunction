using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.Repositorys
{
    public class DangkyRepository : IDangkyRepository
    {
        private readonly UehDbContext _context;

        public DangkyRepository(UehDbContext context)
        {
            _context = context;
        }


        public async Task<List<GiangvienRequest>> GetGiangvienListFromDangky(string madot, string makhoa)
        {
            var giangVienList = await _context.Dangkys
                .Where(p => p.status == "true" && p.madot == madot)
                .Join(_context.Giangviens, p => p.magv, gk => gk.magv, (p, gk) => new { Phancong = p, Giangviens = gk })
                .Where(pgk => pgk.Giangviens.makhoa == makhoa)
                .GroupBy(pgk => pgk.Phancong.magv)
                .Select(g => new GiangvienRequest
                {
                    MaGiangVien = g.Key,
                    TenGiangVien = g.First().Phancong.giangvien.tengv,
                    SoSinhVienHuongDan = g.Count()
                }).OrderByDescending(t => t.TenGiangVien)
                .ToListAsync();

            foreach (var giangVien in giangVienList)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.userId == giangVien.MaGiangVien);
                if (user != null)
                {
                    giangVien.Email = user.email;
                    giangVien.SDT = user.sdt;
                }
            }

            return giangVienList;
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
            var Dangkykhoa = await _context.Dangkys.Where(a => a.mssv == Dangky.mssv && a.madot == Dangky.madot).FirstOrDefaultAsync();

            if (Dangkykhoa == null)
                _context.Add(Dangky);
            return await Save();
        }

        public async Task<bool> DeleteDangky(string madot, string mssv)
        {
            var dangky = await _context.Dangkys.FirstOrDefaultAsync(d => d.mssv == mssv && d.madot == madot);
            if (dangky != null)
                _context.Remove(dangky);
            return await Save();
        }


        public async Task<Dangky> GetDangky(string madot, string mssv)
        {
            return await _context.Dangkys.Where(s => s.madot == madot && s.mssv == mssv && s.status == "true").FirstOrDefaultAsync();
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


                        for (int row = 2; row <= rowCount; row++)
                        {
                            var mssv = worksheet.Cells[row, 1].Value?.ToString();
                            bool existing = await _context.Dangkys.AnyAsync(s => s.mssv == mssv && s.status == "true" && s.madot == madot);

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
                                makhoa = makhoa,
                                status = "true"
                            };

                            await _context.Dangkys.AddAsync(dangky);
                        }

                        return await Save();
                    }
                }

            }
            return false;
        }

        public async Task<byte[]> ExportToExcel(string madot, string makhoa)
        {
            var sinhviens = await _context.Sinhviens
                .Where(sv => sv.madot == madot && sv.makhoa == makhoa)
                .OrderBy(sv => sv.mssv)
                .ToListAsync();

            // Tạo một package Excel và ghi dữ liệu vào worksheet
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DangKy");

                // Đặt tiêu đề cho các cột
                worksheet.Cells["A1"].Value = "MSSV";
                worksheet.Cells["B1"].Value = "Họ";
                worksheet.Cells["C1"].Value = "Tên ";
                worksheet.Cells["D1"].Value = "Ngày sinh";
                worksheet.Cells["E1"].Value = "Mã Lớp";
                worksheet.Cells["F1"].Value = "Mã Loại";
                worksheet.Cells["G1"].Value = "Chuyên ngành";
                worksheet.Cells["H1"].Value = "Mã Gvhd";
                worksheet.Cells["I1"].Value = "Giáo viên hướng dẫn";

                // Ghi dữ liệu vào worksheet
                int rowIndex = 2;
                foreach (var sinhvien in sinhviens)
                {
                    worksheet.Cells[$"A{rowIndex}"].Value = sinhvien.mssv;
                    worksheet.Cells[$"B{rowIndex}"].Value = sinhvien.ho;
                    worksheet.Cells[$"C{rowIndex}"].Value = sinhvien.ten;
                    worksheet.Cells[$"D{rowIndex}"].Value = sinhvien.ngaysinh ?? "";
                    worksheet.Cells[$"E{rowIndex}"].Value = sinhvien.malop ?? "";
                    worksheet.Cells[$"F{rowIndex}"].Value = sinhvien.maloai;
                    worksheet.Cells[$"G{rowIndex}"].Value = sinhvien.macn;

                    var dangky = await _context.Dangkys
                        .Include(d => d.giangvien)
                        .FirstOrDefaultAsync(d => d.mssv == sinhvien.mssv && d.madot == madot && d.makhoa == makhoa && d.status == "true");

                    worksheet.Cells[$"H{rowIndex}"].Value = dangky?.magv ?? ""; // Nếu không có bản ghi "Dangky" tương ứng, giá trị "magv" sẽ được để trống
                    worksheet.Cells[$"I{rowIndex}"].Value = dangky?.giangvien?.tengv ?? "";

                    rowIndex++;
                }

                worksheet.Cells.AutoFitColumns();

                // Xuất file Excel
                var content = package.GetAsByteArray();
                return content;
            }
        }
    }
}

