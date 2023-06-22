
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SelectPdf;
using System.Text;
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

                    float count = (float)((ketqua.tieuchi1 ?? 0) + (ketqua.tieuchi2 ?? 0) + (ketqua.tieuchi3 ?? 0) + (ketqua.tieuchi4 ?? 0) + (ketqua.tieuchi5 ?? 0) + (ketqua.tieuchi6 ?? 0) + (ketqua.tieuchi7 ?? 0));
                    if (ketqua.phancong.maloai == "HKDN")
                    {
                        count = (count + (ketqua.diemDN ?? 0)) / 2;
                    }
                    if (count >= 10)
                    {
                        count = 10;
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

        public async Task<byte[]> GeneratePdfByGv(string magv)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create a new PDF page
            PdfPage page = document.AddPage();

            // Create a new HTML to PDF converter
            HtmlToPdf converter = new HtmlToPdf();

            // Set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            // Load the HTML content from a string
            StringBuilder htmlBuilder = new StringBuilder();

            // Append the HTML content before the table
            htmlBuilder.AppendLine(@"<style>
        .title {
            display: flex; justify-content: space-between;
        }
        .content{
            text-align: center;
        }
        .lable {
        font-size: 25px;
        }

        table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
        }

        .table-title{
        font-size: 18px;
        background-color: rgb(172, 172, 172);
        }
         
    </style>
    <div class=""title"">
         <div>
            <p><strong>Trường Đại học Kinh tế Tp. Hồ Chí Minh</strong></p>
            <p><strong>Khoa Công nghệ Thông tin Kinh doanh</strong></p>
            <p><strong>Chuyên ngành: .............................................</strong></p>
        </div>
        <div>
            <p><strong>Cộng Hòa Xã Hội Chủ Nghĩa Việt Nam</strong></p>
            <p style=""text-align: center;""><strong >Độc lập – Tự do – Hạnh phúc</strong></p>
            <p><strong>&nbsp;</strong></p>
        </div>
    </div>
    <div class=""content"">
        <p class=""lable""><strong>BẢNG ĐIỂM TỔNG HỢP - THỰC TẬP TỐT NGHIỆP</strong></p>
        <p class=""lable""><strong>ĐỢT
            </strong>..........<strong>
                Hình thức:
            </strong>............
        </p>
        <p><em>(Lưu ý: nếu loại hình thức ""học kỳ doanh nghiệp"" thì không có giáo viên chấm 2)</em></p>
    </div>
    <div>
        <table width=""100%"">
            <tr class=""table-title"">
                <td><strong>STT</strong></td>
                <td><strong>Mã số sinh viên|Lớp|Khoa</strong></td>
                <td><strong>Tên sinh viên</strong></td>
                <td><strong>Tên đề tài</strong></td>
                <td><strong>Điểm cuối cùng</strong></td>
            </tr>");

            // Retrieve data from the database
            List<Ketqua> ketquaList = await _context.Ketquas
                .Include(k => k.phancong)
                .ThenInclude(p => p.sinhvien)
                .Include(k => k.phancong)
                .ThenInclude(p => p.chitiets)
                .Where(k => k.phancong.magv == magv)
                .ToListAsync();


            // Populate data in the table
            for (int i = 0; i < ketquaList.Count; i++)
            {
                Ketqua ketqua = ketquaList[i];
                string? tendetai = ketqua.phancong.chitiets.FirstOrDefault()?.tendetai;

                float count = (float)((ketqua.tieuchi1 ?? 0) + (ketqua.tieuchi2 ?? 0) + (ketqua.tieuchi3 ?? 0) + (ketqua.tieuchi4 ?? 0) + (ketqua.tieuchi5 ?? 0) + (ketqua.tieuchi6 ?? 0) + (ketqua.tieuchi7 ?? 0));
                if (ketqua.phancong.maloai == "HKDN")
                {
                    count = (count + (ketqua.diemDN ?? 0)) / 2;
                }
                if (count >= 10)
                {
                    count = 10;
                }
                htmlBuilder.AppendLine("<tr>");
                htmlBuilder.AppendLine($"<td>{i + 1}</td>");
                htmlBuilder.AppendLine($"<td>{ketqua.phancong.sinhvien.mssv + "|" + ketqua.phancong.sinhvien.malop + "|" + ketqua.phancong.sinhvien.khoahoc}</td>");
                htmlBuilder.AppendLine($"<td>{ketqua.phancong.sinhvien.firstName + " " + ketqua.phancong.sinhvien.lastName}</td>");
                htmlBuilder.AppendLine($"<td>{tendetai}</td>");
                htmlBuilder.AppendLine($"<td>{count}</td>");
                htmlBuilder.AppendLine("</tr>");
            }
            htmlBuilder.AppendLine(@"</table></div>");
            htmlBuilder.AppendLine(@"
            <div>
                <p><strong>Ngày chấm :</strong></p>
                <p><strong>Giáo viên hướng dẫn và chấm 1:</strong></p>
                <p><strong>Giáo viên chấm 2:</strong></p>

            </div>
            ");
            string htmlString = htmlBuilder.ToString();

            // Convert HTML to PDF
            PdfDocument pdfDocument = converter.ConvertHtmlString(htmlString);

            // Save the PDF document to a memory stream
            MemoryStream stream = new MemoryStream();
            pdfDocument.Save(stream);
            stream.Position = 0;

            // Convert the memory stream to a byte array
            byte[] pdfBytes = stream.ToArray();

            // Clean up resources
            stream.Dispose();
            pdfDocument.Close();

            return pdfBytes;
        }


    }
}
