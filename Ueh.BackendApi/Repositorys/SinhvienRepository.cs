using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ServiceStack;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Migrations;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.Repositorys
{
    public class SinhvienRepository : ISinhvienRepository
    {
        private readonly UehDbContext _context;

        public SinhvienRepository(UehDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateSinhvien(string makhoa, Sinhvien sinhvien)
        {
            bool sinhvienkhoa = await _context.Khoas.AnyAsync(a => a.makhoa == makhoa);
            //var giangvienchuyennganh = _context.Chuyennganhs.Where(a => a.macn == Giangvien.macn).FirstOrDefault();
            var gvkhoa = new SinhvienKhoa
            {
                makhoa = makhoa,
                mssv = sinhvien.mssv
            };

            if (sinhvienkhoa)
                _context.Add(gvkhoa);
            _context.Add(sinhvien);

            return await Save();
        }

        public Task<bool> DeleteSinhvien(Sinhvien sinhvien)
        {
            sinhvien.status = "false";
            _context.Update(sinhvien);
            return Save();
        }



        public async Task<Sinhvien> GetSinhvien(string mssv)
        {
            return await _context.Sinhviens.Where(s => s.mssv == mssv && s.status == "true").FirstOrDefaultAsync();
        }

        public async Task<Sinhvien> GetSinhvienName(string name)
        {
            return await _context.Sinhviens.Where(s => s.ten == name && s.status == "true").FirstOrDefaultAsync();

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

        public Task<bool> UpdateSinhvien(Sinhvien sinhvien)
        {
            _context.Update(sinhvien);
            return Save();
        }
        public async Task<bool> ImportExcelFile(IFormFile formFile, string dot, string makhoa)
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
                        // Danh sách tạm thời để lưu trữ các giá trị mssv đã đọc từ file Excel
                        List<string> existingMssv = new List<string>();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var mssv = worksheet.Cells[row, 1].Value?.ToString();
                            bool existing = await _context.Sinhviens.AnyAsync(s => s.mssv == mssv && s.status == "true");

                            // Kiểm tra sự trùng lặp trong danh sách tạm thời
                            if (existingMssv.Contains(mssv) || existing)
                            {
                                continue;
                            }

                            // Thêm mssv vào danh sách tạm thời
                            existingMssv.Add(mssv);

                            var macn = worksheet.Cells[row, 9].Value?.ToString();
                            if (macn != null)
                            {
                                macn = macn.Substring(macn.Length - 2).Trim();
                            }

                            var sinhvien = new Sinhvien
                            {
                                mssv = mssv,
                                ho = worksheet.Cells[row, 2].Value?.ToString(),
                                ten = worksheet.Cells[row, 3].Value?.ToString(),
                                thuoclop = worksheet.Cells[row, 4].Value?.ToString(),
                                khoagoc = worksheet.Cells[row, 5].Value?.ToString(),
                                khoahoc = worksheet.Cells[row, 6].Value?.ToString(),
                                mahp = worksheet.Cells[row, 7].Value?.ToString(),
                                malhp = worksheet.Cells[row, 8].Value?.ToString(),
                                tenhp = worksheet.Cells[row, 9].Value?.ToString(),
                                soct = worksheet.Cells[row, 10].Value?.ToString(),
                                malop = worksheet.Cells[row, 11].Value?.ToString(),
                                bacdt = worksheet.Cells[row, 12].Value?.ToString(),
                                loaihinh = worksheet.Cells[row, 13].Value?.ToString(),
                                macn = macn,
                                madot = dot,
                                status = "true"
                            };



                            var svkhoa = new SinhvienKhoa()
                            {
                                makhoa = makhoa,
                                mssv = mssv,
                            };

                            _context.Add(svkhoa);
                            await _context.Sinhviens.AddAsync(sinhvien);

                        }
                        return await Save();
                    }
                }
            }

            return false;
        }

        public async Task<Khoa> GetKhoaBySinhvien(string mssv)
        {
            var sinhvienKhoa = await _context.SinhvienKhoas
                .Include(sk => sk.khoa)
                .FirstOrDefaultAsync(sk => sk.mssv == mssv);

            if (sinhvienKhoa != null)
            {
                return sinhvienKhoa.khoa;
            }

            return null;
        }

        public async Task<List<SinhvienInfoRequest>> GetDsSinhvienOfKhoa(string madot, string makhoa)
        {
            var sinhvienList = await _context.Sinhviens
                .Where(s => s.status == "true" && s.madot == madot && s.sinhvienkhoas.Any(sk => sk.makhoa == makhoa))
                .OrderBy(s => s.mssv)
                .ToListAsync();

            var sinhvienInfoList = sinhvienList.Select(s => new SinhvienInfoRequest
            {
                mssv = s.mssv,
                ho = s.ho,
                ten = s.ten,
                thuoclop = s.thuoclop,
                khoagoc = s.khoagoc,
                email = GetSinhvienEmail(s.mssv)
            }).ToList();

            return sinhvienInfoList;
        }

        private string GetSinhvienEmail(string mssv)
        {
            var user = _context.Users.FirstOrDefault(u => u.userId == mssv);
            return user != null ? user.email : null;
        }








        public async Task<Giangvien> GetDangkyGvHuongDanSv(string madot, string mssv)
        {
            var giangvien = await _context.Dangkys
                 .Include(p => p.giangvien)
                 .Where(p => p.mssv == mssv && p.madot == madot && p.status == "true")
                 .Select(p => p.giangvien)
                 .FirstOrDefaultAsync();

            return giangvien;
        }

        public async Task<Giangvien> GetGvHuongDanSv(string madot, string mssv)
        {
            var giangvien = await _context.Phancongs
                .Include(p => p.giangvien)
                .Where(p => p.mssv == mssv && p.madot == madot && p.status == "true")
                .Select(p => p.giangvien)
                .FirstOrDefaultAsync();


            return giangvien;
        }

        public async Task<string> GetLoaiHinhThucTap(string mssv)
        {
            var phancong = await _context.Phancongs
                .Include(p => p.loai)
                .FirstOrDefaultAsync(p => p.mssv == mssv);

            if (phancong != null)
            {
                return phancong.loai?.name;
            }

            return null;
        }


        public async Task<List<SinhvienInfoRequest>> SearchSinhVien(string madot, string keyword)
        {
            string lowerKeyword = keyword.ToLower();

            var searchResults = await _context.Sinhviens
                .Where(sv => sv.madot.Contains(madot) && sv.status.Contains("true") && sv.ten.ToLower().Contains(lowerKeyword) || sv.mssv.ToLower().Contains(lowerKeyword))
                .ToListAsync();

            var sinhvienInfoList = searchResults.Select(sv => new SinhvienInfoRequest
            {
                mssv = sv.mssv,
                ho = sv.ho,
                ten = sv.ten,
                thuoclop = sv.thuoclop,
                khoagoc = sv.khoagoc,
                email = GetSinhvienEmail(sv.mssv)
            }).ToList();

            return sinhvienInfoList;
        }



    }
}
