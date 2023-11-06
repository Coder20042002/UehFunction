using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ServiceStack;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.Repositorys
{
    public class KetquaRepository : IKetquaRepository
    {
        private readonly UehDbContext _context;

        public KetquaRepository(UehDbContext context)
        {
            _context = context;
        }


        public async Task<ICollection<Ketqua>> GetKetQuaByMaGV(string magv)
        {
            var phanCongIds = await _context.Phancongs.Where(pc => pc.magv == magv && pc.status == "true").Select(pc => pc.Id).ToListAsync();
            var ketQuaList = await _context.Ketquas.Where(kq => phanCongIds.Contains(kq.mapc)).ToListAsync();


            return ketQuaList;
        }

        public async Task<ICollection<Ketqua>> GetScores()
        {
            var phanCongIds = await _context.Phancongs.Where(pc => pc.status == "true").Select(pc => pc.Id).ToListAsync();
            return await _context.Ketquas.Where(kq => phanCongIds.Contains(kq.mapc)).OrderBy(s => s.mapc).ToListAsync();
        }

        public async Task<Ketqua> GetDiemByMssv(string madot,string mssv)
        {
            var phanCongIds = await _context.Phancongs.Where(pc => pc.madot == madot && pc.status == "true" && pc.mssv == mssv).FirstOrDefaultAsync();
            return await _context.Ketquas.Where(kq => kq.mapc == phanCongIds.Id).FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> ScoresExists(Guid mapc)
        {
            var phanCongIds = await _context.Phancongs.Where(pc => pc.status == "true").Select(pc => pc.Id).ToListAsync();
            return await _context.Ketquas.AnyAsync(kq => kq.mapc == mapc && phanCongIds.Contains(kq.mapc));
        }

        public async Task<bool> UpdateDiem(Ketqua updateketqua,string madot, string mssv)
        {
            var phancong = await _context.Phancongs.FirstOrDefaultAsync(p =>p.madot == madot && p.mssv == mssv && p.status == "true");

            if (phancong == null)
            {
                return false;
            }

            var ketqua = await _context.Ketquas.FirstOrDefaultAsync(k => k.mapc == phancong.Id);

            if (ketqua == null)
            {
                return false;
            }

            ketqua.tieuchi1 = updateketqua.tieuchi1;
            ketqua.tieuchi2 = updateketqua.tieuchi2;
            ketqua.tieuchi3 = updateketqua.tieuchi3;
            ketqua.tieuchi4 = updateketqua.tieuchi4;
            ketqua.tieuchi5 = updateketqua.tieuchi5;
            ketqua.tieuchi6 = updateketqua.tieuchi6;
            ketqua.tieuchi7 = updateketqua.tieuchi7;
            ketqua.diemDN = updateketqua.diemDN;

            _context.Ketquas.Update(ketqua);
            return await Save();
        }



        public async Task<byte[]> ExportToExcel()
        {
            var ketquas = await _context.Ketquas
                .Include(k => k.phancong)
                    .ThenInclude(p => p.sinhvien)
                 .Include(k => k.phancong)
                    .ThenInclude(p => p.giangvien)
                .OrderBy(k => k.phancong.sinhvien.ten)
                .ToListAsync();

            // Tạo một package Excel
            using (var package = new ExcelPackage())
            {
                // Tạo một worksheet trong package
                var worksheet = package.Workbook.Worksheets.Add("ketqua");

                // Đặt tiêu đề cho các cột
                worksheet.Cells["A1"].Value = "MSSV";
                worksheet.Cells["B1"].Value = "Họ";
                worksheet.Cells["C1"].Value = "Tên";
                worksheet.Cells["D1"].Value = "Điểm";
                worksheet.Cells["E1"].Value = "Giáo viên hướng dẫn";


                // Ghi dữ liệu vào worksheet
                int rowIndex = 2;
                foreach (var ketqua in ketquas)
                {
                    if (ketqua.phancong.status != "true")
                    {
                        continue; // Bỏ qua bản ghi không có status bằng "true"
                    }

                    double sum = (double)((ketqua.tieuchi1 ?? 0) + (ketqua.tieuchi2 ?? 0) + (ketqua.tieuchi3 ?? 0) + (ketqua.tieuchi4 ?? 0) + (ketqua.tieuchi5 ?? 0) + (ketqua.tieuchi6 ?? 0) + (ketqua.tieuchi7 ?? 0));
                    if (ketqua.phancong.sinhvien.maloai == "HKDN")
                    {
                        sum = (double)(sum * 0.6 + (ketqua.diemDN ?? 0) * 0.4);

                    }
                    if (sum >= 10)
                    {
                        sum = 10;
                    }

                    worksheet.Cells[$"A{rowIndex}"].Value = ketqua.phancong.mssv;
                    worksheet.Cells[$"B{rowIndex}"].Value = ketqua.phancong.sinhvien.ho;
                    worksheet.Cells[$"C{rowIndex}"].Value = ketqua.phancong.sinhvien.ten;
                    worksheet.Cells[$"D{rowIndex}"].Value = Math.Round(sum, 2);
                    worksheet.Cells[$"E{rowIndex}"].Value = ketqua.phancong.giangvien.tengv;


                    rowIndex++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.Cells.AutoFitColumns();

                // Xuất file Excel
                var content = package.GetAsByteArray();
                return content;
            }
        }
        public async Task<byte[]> ExportToExcelByKhoa(string madot, string makhoa)
        {
            var ketquas = await _context.Ketquas
                         .Include(k => k.phancong)
                             .ThenInclude(p => p.sinhvien)
                         .Include(k => k.phancong)
                             .ThenInclude(p => p.giangvien)
                         .Where(k => k.phancong.giangvien.makhoa == makhoa && k.phancong.madot == madot)
                         .OrderBy(k => k.phancong.giangvien.tengv)
                         .ToListAsync();
            // Tạo một package Excel
            using (var package = new ExcelPackage())
            {
                // Tạo một worksheet trong package
                var worksheet = package.Workbook.Worksheets.Add("ketqua");

                // Đặt tiêu đề cho các cột
                worksheet.Cells["A1"].Value = "MSSV";
                worksheet.Cells["B1"].Value = "Họ";
                worksheet.Cells["C1"].Value = "Tên";
                worksheet.Cells["D1"].Value = "Điểm";
                worksheet.Cells["E1"].Value = "Giáo viên hướng dẫn";


                // Ghi dữ liệu vào worksheet
                int rowIndex = 2;
                foreach (var ketqua in ketquas)
                {
                    if (ketqua.phancong.status != "true")
                    {
                        continue;
                    }

                    double sum = (double)((ketqua.tieuchi1 ?? 0) + (ketqua.tieuchi2 ?? 0) + (ketqua.tieuchi3 ?? 0) + (ketqua.tieuchi4 ?? 0) + (ketqua.tieuchi5 ?? 0) + (ketqua.tieuchi6 ?? 0) + (ketqua.tieuchi7 ?? 0));
                    if (ketqua.phancong.sinhvien.maloai == "HKDN")
                    {
                        sum = (double)(sum * 0.6 + (ketqua.diemDN ?? 0) * 0.4);

                    }
                    if (sum >= 10)
                    {
                        sum = 10;
                    }

                    worksheet.Cells[$"A{rowIndex}"].Value = ketqua.phancong.mssv;
                    worksheet.Cells[$"B{rowIndex}"].Value = ketqua.phancong.sinhvien.ho;
                    worksheet.Cells[$"C{rowIndex}"].Value = ketqua.phancong.sinhvien.ten;
                    worksheet.Cells[$"D{rowIndex}"].Value = Math.Round(sum, 2);
                    worksheet.Cells[$"E{rowIndex}"].Value = ketqua.phancong.giangvien.tengv;


                    rowIndex++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.Cells.AutoFitColumns();

                // Xuất file Excel
                var content = package.GetAsByteArray();
                return content;
            }
        }

        public async Task<DiemchitietRequest> DiemChiTietSv(string madot, string mssv)
        {

            var sinhvien = await _context.Sinhviens
              .Include(sk => sk.khoa)
              .FirstOrDefaultAsync(sk => sk.madot == madot && sk.mssv == mssv);

            var phancong = await _context.Phancongs
                .Include(p => p.sinhvien)
                .ThenInclude(s => s.chuyennganh)
                .Include(p => p.chitiets)
                .Include(p => p.giangvien)
                .Include(p => p.dot)
                .FirstOrDefaultAsync(p => p.madot == madot && p.mssv == mssv && p.status == "true");

            if (phancong == null)
            {
                return null;
            }

            var ketqua = await _context.Ketquas
                .FirstOrDefaultAsync(k => k.mapc == phancong.Id);

            if (ketqua == null)
            {
                return null;
            }

            double sum = (double)((ketqua.tieuchi1 ?? 0) + (ketqua.tieuchi2 ?? 0) + (ketqua.tieuchi3 ?? 0) + (ketqua.tieuchi4 ?? 0) + (ketqua.tieuchi5 ?? 0) + (ketqua.tieuchi6 ?? 0) + (ketqua.tieuchi7 ?? 0));
            if (ketqua.phancong.sinhvien.maloai == "HKDN")
            {
                sum = (double)(sum * 0.6 + (ketqua.diemDN ?? 0) * 0.4);
            }
            if (sum >= 10)
            {
                sum = 10;
            }

            var diemChiTietRequest = new DiemchitietRequest
            {
                tenkhoa = sinhvien.khoa.tenkhoa,
                tencn = phancong.sinhvien.chuyennganh.tencn,
                tendot = phancong.dot.tendot,
                hotensv = phancong.sinhvien.ho + " " + phancong.sinhvien.ten,
                mssv = phancong.mssv,
                lop = phancong.sinhvien.malop,
                maloai = phancong.sinhvien.maloai,
                tenkl = phancong.chitiets.FirstOrDefault()?.tendetai,
                tencty = phancong.chitiets.FirstOrDefault()?.tencty,
                tengv = phancong.giangvien.tengv,
                tieuchi1 = ketqua.tieuchi1,
                tieuchi2 = ketqua.tieuchi2,
                tieuchi3 = ketqua.tieuchi3,
                tieuchi4 = ketqua.tieuchi4,
                tieuchi5 = ketqua.tieuchi5,
                tieuchi6 = ketqua.tieuchi6,
                tieuchi7 = ketqua.tieuchi7,
                diemDN = ketqua.diemDN,
                diemtong = Math.Round(sum, 2)
        };

            return diemChiTietRequest;
        }
        public async Task<List<DiemchitietRequest>> GetDanhSachDiemChiTietSv(string madot, string magv)
        {
            // Truy vấn danh sách sinh viên từ bảng phân công

            var danhSachSinhVien = await _context.Phancongs
               .Where(p => p.magv == magv && p.madot == madot && p.status == "true")
               .Select(p => p.sinhvien)
               .ToListAsync();

            List<DiemchitietRequest> danhSachDiemChiTiet = new List<DiemchitietRequest>();


            // Duyệt qua từng sinh viên trong danh sách để lấy thông tin điểm chi tiết
            foreach (Sinhvien sinhvien in danhSachSinhVien)
            {
                DiemchitietRequest diemChiTiet = await DiemChiTietSv(madot, sinhvien.mssv);
                if (diemChiTiet != null)
                {
                    danhSachDiemChiTiet.Add(diemChiTiet);
                }
            }

            return danhSachDiemChiTiet;
        }

        public async Task<ICollection<DsDiemGvHuongDanRequest>> DsDiemGvHuongDanRequest(string madot, string maloai, string magv)
        {
            // Lấy thông tin giảng viên từ mã giảng viên
            var giangvien = await _context.Giangviens.FirstOrDefaultAsync(g => g.magv == magv);
            if (giangvien == null)
            {
                return null;
            }

            // Lấy thông tin khoa từ mã giảng viên
            var khoa = await _context.Giangviens
                .Include(sk => sk.khoa)
                .FirstOrDefaultAsync(sk => sk.magv == magv);

            // Lấy thông tin đợt từ mã đợt
            var dot = await _context.Dots.FirstOrDefaultAsync(d => d.madot == madot);

            // Lấy thông tin loại từ mã loại
            var loai = await _context.Loais.FirstOrDefaultAsync(d => d.maloai == maloai);

            // Lấy thông tin chấm chéo giảng viên từ mã giảng viên
            var chamcheo = await _context.Chamcheos.FirstOrDefaultAsync(c => c.magv1 == magv || c.magv2 == magv);

            // Lấy thông tin giảng viên chấm chéo từ mã giảng viên
            var giangvien1 = await _context.Giangviens.FirstOrDefaultAsync(g => g.magv == chamcheo.magv1);
            var giangvien2 = await _context.Giangviens.FirstOrDefaultAsync(g => g.magv == chamcheo.magv2);

            // Lấy danh sách các bản ghi kết quả từ truy vấn
            List<Ketqua> listketqua = await _context.Ketquas
                .Include(k => k.phancong)
                .ThenInclude(p => p.sinhvien)
                .Include(k => k.phancong)
                .ThenInclude(p => p.chitiets)
                .Where(k => k.phancong.magv == magv && k.phancong.madot == madot && k.phancong.sinhvien.maloai == maloai)
                .OrderByDescending(t => t.phancong.sinhvien.ten)
                .ToListAsync();

            // Tạo danh sách kết quả DsDiemGvHuongDanRequest và điền thông tin
            string hotengv1 = "";
            string hotengv2 = "";
            var dsDiemGvHuongDan = new List<DsDiemGvHuongDanRequest>();

            if (loai.maloai == "KL")
            {
                if (giangvien1.magv == magv)
                {
                    hotengv1 = giangvien1.tengv;
                    hotengv2 = giangvien2.tengv;
                }
                else
                {
                    hotengv1 = giangvien2.tengv;
                    hotengv2 = giangvien1.tengv;
                }
            }
            else
            {
                if (giangvien1.magv == magv)
                {
                    hotengv1 = giangvien1.tengv;
                }
                else
                {
                    hotengv1 = giangvien2.tengv;

                }

            }

            foreach (var ketqua in listketqua)
            {
                double sum = (double)((ketqua.tieuchi1 ?? 0) + (ketqua.tieuchi2 ?? 0) + (ketqua.tieuchi3 ?? 0) + (ketqua.tieuchi4 ?? 0) + (ketqua.tieuchi5 ?? 0) + (ketqua.tieuchi6 ?? 0) + (ketqua.tieuchi7 ?? 0));
                if (ketqua.phancong.sinhvien.maloai == "HKDN")
                {
                    sum = (double)(sum * 0.6 + (ketqua.diemDN ?? 0) * 0.4);
                }
                if (sum >= 10)
                {
                    sum = 10;
                }
                var dsDiemGvHuongDanRequest = new DsDiemGvHuongDanRequest
                {
                    tenkhoa = khoa?.khoa?.tenkhoa,
                    tendot = dot?.tendot,
                    tenloai = loai?.tenloai,
                    hotensv = ketqua?.phancong?.sinhvien?.ho + " " + ketqua?.phancong?.sinhvien?.ten,
                    mssv = ketqua?.phancong?.sinhvien?.mssv,
                    tendetai = ketqua?.phancong?.chitiets?.FirstOrDefault()?.tendetai,
                    tencty = ketqua?.phancong?.chitiets?.FirstOrDefault()?.tencty,
                    malop = ketqua?.phancong?.sinhvien?.malop,
                    hotengv1 = hotengv1,
                    hotengv2 = hotengv2,
                    diemtong = Math.Round(sum, 2).ToString(),
                    ngaycham = DateTime.Today.ToString("dd-MM-yyyy"),
                };

                dsDiemGvHuongDan.Add(dsDiemGvHuongDanRequest);
            }

            return dsDiemGvHuongDan;
        }

    }
}
