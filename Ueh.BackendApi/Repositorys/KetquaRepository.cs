using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Repositorys
{
    public class KetquaRepository : IKetquaRepository
    {
        private readonly UehDbContext _context;

        public KetquaRepository(UehDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> ExportToExcel()
        {
            var ketquas = await _context.Ketquas
                .Include(k => k.phancong)
                    .ThenInclude(p => p.sinhvien)
                .OrderBy(k => k.phancong.sinhvien.lastName)
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


                // Ghi dữ liệu vào worksheet
                int rowIndex = 2;
                foreach (var ketqua in ketquas)
                {
                    if (ketqua.phancong.status != "true")
                    {
                        continue; // Bỏ qua bản ghi không có status bằng "true"
                    }

                    float count = 0;
                    if (ketqua.phancong.maloai == "HKDN" && ketqua.diemDN != null && ketqua.diemGV != null)
                    {
                        count = (float)(ketqua.diemDN + ketqua.diemGV) / 2;
                    }
                    else
                    {
                        count = (float)(ketqua.tieuchi1 + ketqua.tieuchi2 + ketqua.tieuchi3 + ketqua.tieuchi4 + ketqua.tieuchi5 + ketqua.tieuchi6 + ketqua.tieuchi7);
                        if (count >= 10)
                        {
                            count = 10;
                        }
                    }
                    worksheet.Cells[$"A{rowIndex}"].Value = ketqua.phancong.mssv;
                    worksheet.Cells[$"B{rowIndex}"].Value = ketqua.phancong.sinhvien.firstName;
                    worksheet.Cells[$"C{rowIndex}"].Value = ketqua.phancong.sinhvien.lastName;
                    worksheet.Cells[$"D{rowIndex}"].Value = count;


                    rowIndex++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.Cells.AutoFitColumns();

                // Xuất file Excel
                var content = package.GetAsByteArray();
                return content;
            }
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

        public async Task<Ketqua> GetScores(Guid mapc)
        {
            var phanCongIds = await _context.Phancongs.Where(pc => pc.status == "true").Select(pc => pc.Id).ToListAsync();
            return await _context.Ketquas.Where(kq => kq.mapc == mapc && phanCongIds.Contains(kq.mapc)).FirstOrDefaultAsync();
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

        public Task<bool> UpdateScores(Ketqua ketqua)
        {
            _context.Update(ketqua);
            return Save();
        }
    }
}
