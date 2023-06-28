using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;

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


    }

}
