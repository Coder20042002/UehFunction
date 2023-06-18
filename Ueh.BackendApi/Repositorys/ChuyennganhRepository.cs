using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Repositorys
{
    public class ChuyennganhRepository : IChuyennganhRepository
    {
        private readonly UehDbContext _context;

        public ChuyennganhRepository(UehDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateChuyennganh(Chuyennganh Chuyennganh)
        {
            var chuyennganhExist = await _context.Chuyennganhs.Where(a => a.macn == Chuyennganh.macn).FirstOrDefaultAsync();

            if (chuyennganhExist == null)
                _context.Add(Chuyennganh);

            return await Save();
        }

        public async Task<bool> DeleteChuyennganh(Chuyennganh Chuyennganh)
        {
            _context.Remove(Chuyennganh);
            return await Save();
        }


        public async Task<Chuyennganh> GetChuyennganh(string macn)
        {
            return await _context.Chuyennganhs.Where(s => s.macn == macn).FirstOrDefaultAsync();
        }

        public async Task<Chuyennganh> GetChuyennganhName(string name)
        {
            return await _context.Chuyennganhs.Where(s => s.tencn == name).FirstOrDefaultAsync();

        }

        public async Task<ICollection<Chuyennganh>> GetChuyennganhs()
        {
            return await _context.Chuyennganhs.OrderBy(s => s.macn).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> ChuyennganhExists(string macn)
        {
            return await _context.Chuyennganhs.AnyAsync(s => s.macn == macn);
        }

        public async Task<bool> UpdateChuyennganh(Chuyennganh Chuyennganh)
        {
            bool ChuyennganhExists = await _context.Chuyennganhs.AnyAsync(s => s.macn == Chuyennganh.macn);

            if (ChuyennganhExists != false)
                _context.Update(Chuyennganh);
            return await Save();
        }

        public async Task<bool> ImportExcelFile(IFormFile formFile)
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
                        for (int row = 2; row <= rowCount; row++)
                        {

                            var chuyennganh = new Chuyennganh
                            {
                                macn = worksheet.Cells[row, 2].Value?.ToString(),
                                tencn = worksheet.Cells[row, 3].Value?.ToString(),
                            };

                            await _context.Chuyennganhs.AddAsync(chuyennganh);
                        }

                        return await Save();
                    }
                }

            }
            return false;
        }

        public async Task<byte[]> ExportToExcel()
        {
            var Chuyennganhs = await _context.Chuyennganhs.ToListAsync();

            // Tạo một package Excel
            using (var package = new ExcelPackage())
            {
                // Tạo một worksheet trong package
                var worksheet = package.Workbook.Worksheets.Add("Chuyennganh");

                // Đặt tiêu đề cho các cột
                worksheet.Cells["A1"].Value = "STT";
                worksheet.Cells["B1"].Value = "macn";
                worksheet.Cells["C1"].Value = "Tên Chuyên ngành";


                // Ghi dữ liệu vào worksheet
                int rowIndex = 2;
                int count = 0;
                foreach (var Chuyennganh in Chuyennganhs)
                {
                    worksheet.Cells[$"A{rowIndex}"].Value = count++;
                    worksheet.Cells[$"B{rowIndex}"].Value = Chuyennganh.macn;
                    worksheet.Cells[$"C{rowIndex}"].Value = Chuyennganh.tencn;


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
