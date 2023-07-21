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
            var ketquaList = await _context.Ketquas
                .Include(k => k.phancong)
                .ThenInclude(p => p.sinhvien)
                .Where(k => k.phancong.magv == magv && k.phancong.madot == madot)
                .Select(k => new
                {
                    k.phancong.sinhvien,
                    k.tieuchi1,
                    k.tieuchi2,
                    k.tieuchi3,
                    k.tieuchi4,
                    k.tieuchi5,
                    k.tieuchi6,
                    k.tieuchi7,
                    k.diemDN,
                    k.phancong.maloai
                })
                .ToListAsync();

            var ketquaRequestList = new List<KetquaRequest>();

            foreach (var ketqua in ketquaList)
            {
                var sinhvien = ketqua.sinhvien;
                var email = GetSinhvienEmail(sinhvien.mssv);

                var ketquaRequest = new KetquaRequest
                {
                    TenSinhVien = $"{sinhvien.ho} {sinhvien.ten}",
                    MaSinhVien = sinhvien.mssv,
                    Lop = sinhvien.thuoclop,
                    Khoa = sinhvien.khoagoc,
                    Email = email,
                    Diem = ketqua.maloai == "HKDN" ?
                        (double)(((ketqua.tieuchi1 ?? 0) + (ketqua.tieuchi2 ?? 0) + (ketqua.tieuchi3 ?? 0) + (ketqua.tieuchi4 ?? 0) + (ketqua.tieuchi5 ?? 0) + (ketqua.tieuchi6 ?? 0) + (ketqua.tieuchi7 ?? 0)) * 0.6 + ((ketqua.diemDN ?? 0) * 0.4))
                        : ((ketqua.tieuchi1 ?? 0) + (ketqua.tieuchi2 ?? 0) + (ketqua.tieuchi3 ?? 0) + (ketqua.tieuchi4 ?? 0) + (ketqua.tieuchi5 ?? 0) + (ketqua.tieuchi6 ?? 0) + (ketqua.tieuchi7 ?? 0))
                };

                ketquaRequestList.Add(ketquaRequest);
            }

            return ketquaRequestList;
        }
        public async Task<List<SinhvienInfoRequest>> GetSinhVienByGiangVien(string madot, string magv)
        {
            var sinhvienList = await _context.Phancongs
                .Where(p => p.magv == magv && p.madot == madot && p.status == "true")
                .Select(p => p.sinhvien)
                .OrderByDescending(t => t.ten)
                .ToListAsync();

            var sinhvienInfoList = sinhvienList.Select(sv => new SinhvienInfoRequest
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

        private string GetSinhvienEmail(string mssv)
        {
            var user = _context.Users.FirstOrDefault(u => u.userId == mssv);
            return user != null ? user.email : null;
        }

        public async Task<List<GiangvienRequest>> GetGiangVienAndSinhVienHuongDan(string madot, string makhoa)
        {
            var giangVienList = await _context.Phancongs
                .Where(p => p.status == "true" && p.madot == madot)
                .Join(_context.Giangviens, p => p.magv, gk => gk.magv, (p, gk) => new { Phancong = p, Giangvien = gk })
                .Where(pgk => pgk.Giangvien.makhoa == makhoa)
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
            List<GiangvienUpdateRequest> giangviens = await _context.Giangviens
                .Where(gvk => gvk.makhoa == makhoa && gvk.status == "true")
                .Select(gvk => new GiangvienUpdateRequest
                {
                    magv = gvk.magv,
                    tengv = gvk.tengv,
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


        public async Task<bool> CreateGiangvien(string makhoa, GiangvienUpdateRequest giangvienupdate)
        {

            var kiemtrauser = await _context.Users.AnyAsync(g => g.userId == giangvienupdate.magv);

            if (kiemtrauser == false)
            {
                var user = new User
                {
                    userId = giangvienupdate.magv,
                    email = giangvienupdate.email,
                    sdt = giangvienupdate.sdt,
                    role = "teacher"

                };
                _context.Add(user);

            }

            var giangvien = new Giangvien
            {
                magv = giangvienupdate.magv,
                makhoa = makhoa,
                tengv = giangvienupdate.tengv,
                status = "true"

            };

            await _context.Giangviens.AddAsync(giangvien);
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

                        for (int row = 2; row <= rowCount; row++)
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
                                makhoa = makhoa
                            };

                            var kiemtrauser = await _context.Users.AnyAsync(g => g.userId == magv);

                            if (kiemtrauser == false)
                            {
                                var user = new User
                                {
                                    userId = magv,
                                    email = worksheet.Cells[row, 4].Value?.ToString(),
                                    sdt = worksheet.Cells[row, 3].Value?.ToString(),
                                    role = "teacher"

                                };
                                _context.Add(user);

                            }





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

