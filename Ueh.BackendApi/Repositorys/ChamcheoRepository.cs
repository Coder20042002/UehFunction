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
                                makhoa = makhoa,
                                madot = madot
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
            var pairs = await _context.Chamcheos.Where(c => c.makhoa == makhoa).ToListAsync();

            var chamcheoRequests = new List<ChamcheoRequest>();

            foreach (var pair in pairs)
            {
                var giangvien1 = await _context.Giangviens.FirstOrDefaultAsync(gv => gv.magv == pair.magv1);
                if (giangvien1 != null)
                {
                    var user1 = await _context.Users.FirstOrDefaultAsync(u => u.userId == giangvien1.magv);
                    if (user1 != null)
                    {
                        var chamcheoRequest = new ChamcheoRequest
                        {
                            magv1 = giangvien1.magv,
                            tengv1 = giangvien1.tengv,
                            email1 = user1.email
                        };

                        var giangvien2 = await _context.Giangviens.FirstOrDefaultAsync(gv => gv.magv == pair.magv2);
                        if (giangvien2 != null)
                        {
                            var user2 = await _context.Users.FirstOrDefaultAsync(u => u.userId == giangvien2.magv);
                            if (user2 != null)
                            {
                                chamcheoRequest.magv2 = giangvien2.magv;
                                chamcheoRequest.tengv2 = giangvien2.tengv;
                                chamcheoRequest.email2 = user2.email;
                            }
                        }

                        chamcheoRequests.Add(chamcheoRequest);
                    }
                }
            }

            return chamcheoRequests;
        }

    }

}
