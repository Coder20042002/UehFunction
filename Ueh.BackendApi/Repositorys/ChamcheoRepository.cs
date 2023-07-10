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

        public async Task<List<ChamcheoRequest>> GetChamcheoByGiangVien(string madot, string makhoa)
        {
            var pairs = await _context.Chamcheos.Where(c => c.makhoa == makhoa && c.madot == madot).ToListAsync();

            var chamcheoRequests = new List<ChamcheoRequest>();

            foreach (var pair in pairs)
            {
                var giangvien1 = await _context.Giangviens.FirstOrDefaultAsync(gv => gv.magv == pair.magv1);
                var giangvien2 = await _context.Giangviens.FirstOrDefaultAsync(gv => gv.magv == pair.magv2);
                var user1 = await _context.Users.FirstOrDefaultAsync(u => u.userId == pair.magv1);
                var user2 = await _context.Users.FirstOrDefaultAsync(u => u.userId == pair.magv2);

                if (user1 != null && user2 != null)
                {
                    var chamcheoRequest = new ChamcheoRequest
                    {
                        magv1 = pair.magv1,
                        magv2 = pair.magv2,
                        tengv1 = giangvien1 != null ? giangvien1.tengv : string.Empty,
                        tengv2 = giangvien2 != null ? giangvien2.tengv : string.Empty,
                        email1 = user1.email,
                        email2 = user2.email
                    };

                    chamcheoRequests.Add(chamcheoRequest);
                }
                else
                {
                    var chamcheoRequest = new ChamcheoRequest
                    {
                        magv1 = pair.magv1,
                        magv2 = pair.magv2,
                        tengv1 = giangvien1 != null ? giangvien1.tengv : string.Empty,
                        tengv2 = giangvien2 != null ? giangvien2.tengv : string.Empty,

                    };

                    chamcheoRequests.Add(chamcheoRequest);
                }


            }

            return chamcheoRequests;
        }


        public async Task<byte[]> ExportToExcel(string madot, string makhoa)
        {
            var pairs = await _context.Chamcheos.Where(c => c.makhoa == makhoa && c.madot == madot).ToListAsync();

            var chamcheoRequests = new List<ChamcheoRequest>();

            foreach (var pair in pairs)
            {
                var giangvien1 = await _context.Giangviens.FirstOrDefaultAsync(gv => gv.magv == pair.magv1);
                var giangvien2 = await _context.Giangviens.FirstOrDefaultAsync(gv => gv.magv == pair.magv2);
                var user1 = await _context.Users.FirstOrDefaultAsync(u => u.userId == pair.magv1);
                var user2 = await _context.Users.FirstOrDefaultAsync(u => u.userId == pair.magv2);

                var chamcheoRequest = new ChamcheoRequest
                {
                    magv1 = pair.magv1,
                    magv2 = pair.magv2,
                    tengv1 = giangvien1 != null ? giangvien1.tengv : string.Empty,
                    tengv2 = giangvien2 != null ? giangvien2.tengv : string.Empty,

                };

                chamcheoRequests.Add(chamcheoRequest);
            }




            // Tạo một package Excel
            using (var package = new ExcelPackage())
            {
                // Tạo một worksheet trong package
                var worksheet = package.Workbook.Worksheets.Add("Chamcheo");

                // Đặt tiêu đề cho các cột
                worksheet.Cells["A1"].Value = "Họ tên giáo viên 1";
                worksheet.Cells["B1"].Value = "Họ tên giáo viên 2";

                // Ghi dữ liệu vào worksheet
                int rowIndex = 2;
                int count = 0;
                foreach (var chamcheo in chamcheoRequests)
                {

                    worksheet.Cells[$"A{rowIndex}"].Value = chamcheo.tengv1;
                    worksheet.Cells[$"B{rowIndex}"].Value = chamcheo.tengv2;


                    rowIndex++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.Cells.AutoFitColumns();

                // Xuất file Excel
                var content = package.GetAsByteArray();
                return content;
            }
        }

    }

}
