using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ServiceStack.Text;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.Repositorys
{
    public class GiangvienRepository : IGiangvienRepository
    {
        private readonly UehDbContext _context;

        public GiangvienRepository(UehDbContext context)
        {
            _context = context;
        }
        public async Task<List<KetquaRequest>> GetDanhSachDiem(string madot, string magv)
        {
            List<KetquaRequest> ketquaList = await _context.Ketquas
                   .Include(k => k.phancong)
                   .ThenInclude(p => p.sinhvien)
                   .Where(k => k.phancong.magv == magv && k.phancong.madot == madot)
                   .Select(k => new KetquaRequest
                   {
                       TenSinhVien = $"{k.phancong.sinhvien.ho} {k.phancong.sinhvien.ten}",
                       MaSinhVien = k.phancong.sinhvien.mssv,
                       Lop = k.phancong.sinhvien.thuoclop,
                       Khoa = k.phancong.sinhvien.khoagoc,
                       Diem = k.phancong.maloai == "HKDN" ?
                       (double)(((k.tieuchi1 ?? 0) + (k.tieuchi2 ?? 0) + (k.tieuchi3 ?? 0) + (k.tieuchi4 ?? 0) + (k.tieuchi5 ?? 0) + (k.tieuchi6 ?? 0) + (k.tieuchi7 ?? 0)) * 0.6 + ((k.diemDN ?? 0) * 0.4))
                           : ((k.tieuchi1 ?? 0) + (k.tieuchi2 ?? 0) + (k.tieuchi3 ?? 0) + (k.tieuchi4 ?? 0) + (k.tieuchi5 ?? 0) + (k.tieuchi6 ?? 0) + (k.tieuchi7 ?? 0))
                   }).OrderByDescending(t => t.TenSinhVien)

           .ToListAsync();


            return ketquaList;
        }
        public async Task<List<Sinhvien>> GetSinhVienByGiangVien(string madot, string magv)
        {
            return await _context.Phancongs
                .Where(p => p.magv == magv && p.madot == madot)
                .Select(p => p.sinhvien)
                .OrderByDescending(t => t.ten)
                .ToListAsync();
        }
        public async Task<List<GiangvienRequest>> GetGiangVienAndSinhVienHuongDan(string madot, string makhoa)
        {
            var giangVienList = await _context.Phancongs
                .Where(p => p.status == "true" && p.madot == madot)
                .Join(_context.GiangvienKhoas, p => p.magv, gk => gk.magv, (p, gk) => new { Phancong = p, GiangvienKhoa = gk })
                .Where(pgk => pgk.GiangvienKhoa.makhoa == makhoa)
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




        public async Task<List<GiangvienUpdateRequest>> GetGiangvienByKhoa(string makhoa)
        {
            List<GiangvienUpdateRequest> giangviens = await _context.GiangvienKhoas
                .Include(gvk => gvk.giangvien)
                .Where(gvk => gvk.makhoa == makhoa && gvk.giangvien.status == "true")
                .Select(gvk => new GiangvienUpdateRequest
                {
                    magv = gvk.giangvien.magv,
                    tengv = gvk.giangvien.tengv,
                }).OrderByDescending(t => t.tengv)

                .ToListAsync();

            foreach (var giangvien in giangviens)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.userId == giangvien.magv);
                if (user != null)
                {
                    giangvien.email = user.email;
                    giangvien.sdt = user.sdt;
                }
            }

            return giangviens;
        }


        public async Task<bool> CreateGiangvien(string makhoa, Giangvien Giangvien)
        {
            bool giangvienkhoa = await _context.Khoas.AnyAsync(a => a.makhoa == makhoa);
            //var giangvienchuyennganh = _context.Chuyennganhs.Where(a => a.macn == Giangvien.macn).FirstOrDefault();
            var gvkhoa = new GiangvienKhoa
            {
                makhoa = makhoa,
                magv = Giangvien.magv,
            };

            if (giangvienkhoa)
                _context.Add(gvkhoa);
            _context.Add(Giangvien);

            return await Save();
        }

        public async Task<bool> DeleteGiangvien(string magv)
        {
            var giangvien = await _context.Giangviens.FirstOrDefaultAsync(g => g.magv == magv);
            if (giangvien == null)
            {
                return false;
            }

            giangvien.status = "false";

            return await Save();
        }




        public async Task<GiangvienUpdateRequest> GetThongtinGiangvien(string magv)
        {
            var usergv = await _context.Users.FirstOrDefaultAsync(u => u.userId == magv);
            var giangvien = await _context.Giangviens.FirstOrDefaultAsync(u => u.magv == magv && u.status == "true");

            if (usergv != null)
            {
                var thongtingv = new GiangvienUpdateRequest
                {
                    magv = giangvien.magv,
                    tengv = giangvien.tengv,
                    chuyenmon = giangvien.chuyenmon,
                    email = usergv.email,
                    sdt = usergv.sdt
                };
                return thongtingv;

            }
            else
            {
                var thongtingv = new GiangvienUpdateRequest
                {
                    magv = giangvien.magv,
                    tengv = giangvien.tengv,
                    chuyenmon = giangvien.chuyenmon

                };
                return thongtingv;

            }

        }

        public async Task<Giangvien> GetGiangvienName(string name)
        {
            return await _context.Giangviens.Where(s => s.tengv == name).FirstOrDefaultAsync();

        }



        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> GiangvienExists(string magv)
        {
            return await _context.Giangviens.AnyAsync(s => s.magv == magv);
        }

        public async Task<bool> UpdateGiangvien(GiangvienUpdateRequest updategiangvien)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.userId == updategiangvien.magv);
            var giangvien = await _context.Giangviens.FirstOrDefaultAsync(g => g.magv == updategiangvien.magv);
            if (user != null)
            {
                user.email = updategiangvien.email;
                user.sdt = updategiangvien.sdt;

            }



            giangvien.magv = updategiangvien.magv;
            giangvien.tengv = updategiangvien.tengv;
            giangvien.chuyenmon = updategiangvien.chuyenmon;


            return await Save();
        }

        public async Task<bool> ImportExcelFile(IFormFile formFile, string makhoa)
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

                            var magv = worksheet.Cells[row, 1].Value?.ToString();
                            bool existing = await _context.Giangviens.AnyAsync(g => g.magv == magv && g.status == "true");

                            if (existing == true)
                            {
                                continue;
                            }
                            var giangvien = new Giangvien
                            {
                                magv = magv,
                                tengv = worksheet.Cells[row, 2].Value?.ToString(),
                                //sdt = worksheet.Cells[row, 3].Value?.ToString(),
                                //email = worksheet.Cells[row, 4].Value?.ToString(),

                            };

                            var khoagv = new GiangvienKhoa
                            {
                                magv = magv,
                                makhoa = makhoa
                            };

                            _context.Add(khoagv);
                            await _context.Giangviens.AddAsync(giangvien);
                        }

                        return await Save();
                    }
                }

            }
            return false;
        }
    }
}

