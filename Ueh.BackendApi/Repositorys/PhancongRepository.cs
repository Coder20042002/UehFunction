using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;

namespace Ueh.BackendApi.Repositorys
{
    public class PhancongRepository : IPhanCongRepository
    {
        private readonly UehDbContext _context;

        public PhancongRepository(UehDbContext context)
        {
            _context = context;
        }

        public Byte[] ExportToExcel()
        {
            // Lấy danh sách phân công từ cơ sở dữ liệu
            var phanCongs = _context.Phancongs.ToList();

            // Tạo một package Excel
            using (var package = new ExcelPackage())
            {
                // Tạo một worksheet trong package
                var worksheet = package.Workbook.Worksheets.Add("PhanCong");

                // Đặt tiêu đề cho các cột
                worksheet.Cells["A1"].Value = "STT";
                worksheet.Cells["B1"].Value = "MSSV";
                worksheet.Cells["C1"].Value = "LopSV";
                worksheet.Cells["D1"].Value = "Ho";
                worksheet.Cells["E1"].Value = "Ten";
                worksheet.Cells["F1"].Value = "NgaySinh";
                worksheet.Cells["G1"].Value = "TenGV";

                // Ghi dữ liệu vào worksheet
                int rowIndex = 2;
                foreach (var phanCong in phanCongs)
                {
                    worksheet.Cells[$"A{rowIndex}"].Value = phanCong.stt;
                    worksheet.Cells[$"B{rowIndex}"].Value = phanCong.mssv;
                    worksheet.Cells[$"C{rowIndex}"].Value = phanCong.lopsv;
                    worksheet.Cells[$"D{rowIndex}"].Value = phanCong.ho;
                    worksheet.Cells[$"E{rowIndex}"].Value = phanCong.ten;
                    worksheet.Cells[$"F{rowIndex}"].Value = phanCong.ngaysinh;
                    worksheet.Cells[$"G{rowIndex}"].Value = phanCong.tengv;

                    rowIndex++;
                }

                // Tự động điều chỉnh kích thước các cột
                worksheet.Cells.AutoFitColumns();

                // Xuất file Excel
                var content = package.GetAsByteArray();
                return content;
            }


        }

        public ICollection<PhanCong> GetPhanCongs()
        {
            return _context.Phancongs.ToList();
        }

        public bool ImportExcelFile(IFormFile formFile)
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
                            double numericDate = Convert.ToDouble(worksheet.Cells[row, 6].Value);
                            DateTime date = DateTime.FromOADate(numericDate);
                            string ngaysinh = date.ToString("MM/dd/yyyy");

                            var phanCong = new PhanCong
                            {
                                Id = Guid.NewGuid(),
                                stt = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                                mssv = worksheet.Cells[row, 2].Value?.ToString(),
                                lopsv = worksheet.Cells[row, 3].Value?.ToString(),
                                ho = worksheet.Cells[row, 4].Value?.ToString(),
                                ten = worksheet.Cells[row, 5].Value?.ToString(),
                                ngaysinh = ngaysinh,
                                tengv = worksheet.Cells[row, 7].Value?.ToString()
                            };

                            _context.Phancongs.AddRange(phanCong);
                        }

                        return Save();
                    }
                }

            }
            return false;
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}
