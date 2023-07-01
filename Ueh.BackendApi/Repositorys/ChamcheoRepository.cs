using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.Repositorys
{
    public class ChamcheoRepository : IChamcheoRepository
    {
        private readonly UehDbContext _context;

        public ChamcheoRepository(UehDbContext context)
        {
            _context = context;
        }
        public async Task<bool> UpdateChamcheo(Chamcheo chamcheo)
        {
            bool ChamcheoExists = await _context.Chamcheos.AnyAsync(s => s.id == chamcheo.id);

            if (ChamcheoExists != false)
                _context.Update(chamcheo);
            return await Save();
        }

        public async Task<bool> ImportExcelFile(IFormFile formFile, string madot, string makhoa)
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
                        for (int row = 1; row <= rowCount; row++)
                        {
                            string magv1 = worksheet.Cells[row, 1].Value?.ToString();
                            string magv2 = worksheet.Cells[row, 2].Value?.ToString();
                            if (magv1 == magv2)
                            {
                                continue;
                            }
                            var chamcheolist = new Chamcheo
                            {
                                id = Guid.NewGuid(),
                                magv1 = magv1,
                                magv2 = magv2,
                                makhoa = madot,
                                madot = makhoa
                            };

                            await _context.Chamcheos.AddAsync(chamcheolist);
                        }

                        return await Save();
                    }
                }

            }
            return false;
        }


        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<List<ChamcheoRequest>> GetChamcheoByGiangVien(string makhoa)
        {
            var chamcheoList = await _context.Chamcheos
                 .Where(cc => cc.makhoa == makhoa)
                 .Join(_context.Giangviens, cc => cc.magv1, gv => gv.magv, (cc, gv) => new { Chamcheo = cc, Giangvien = gv })
                 .Join(_context.Giangviens, ccgv => ccgv.Chamcheo.magv2, gv => gv.magv, (ccgv, gv2) => new { ccgv.Chamcheo, ccgv.Giangvien, Giangvien2 = gv2 })
                 .Select(ccgvv => new ChamcheoRequest
                 {
                     magv1 = ccgvv.Chamcheo.magv1,
                     tengv1 = ccgvv.Giangvien.tengv,
                     email1 = ccgvv.Giangvien.email,
                     magv2 = ccgvv.Chamcheo.magv2,
                     tengv2 = ccgvv.Giangvien2.tengv,
                     email2 = ccgvv.Giangvien2.email
                 })
                 .ToListAsync();
            return chamcheoList;
        }
    }

}
