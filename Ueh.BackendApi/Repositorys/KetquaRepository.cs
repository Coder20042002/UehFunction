
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SelectPdf;
using ServiceStack;
using System.IO.Compression;
using System.Text;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Request;
using CompressionLevel = System.IO.Compression.CompressionLevel;

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

        public async Task<Ketqua> GetDiemByMssv(string mssv)
        {
            var phanCongIds = await _context.Phancongs.Where(pc => pc.status == "true" && pc.mssv == mssv).FirstOrDefaultAsync();
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

        public async Task<bool> UpdateDiem(Ketqua updateketqua, string mssv)
        {
            var phancong = await _context.Phancongs.FirstOrDefaultAsync(p => p.mssv == mssv);

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

        public async Task<byte[]> GeneratePdfByGv(string madot, string maloai, string magv)
        {
            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            HtmlToPdf converter = new HtmlToPdf();

            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            StringBuilder htmlBuilder = new StringBuilder();

            var dot = await _context.Dots.FirstOrDefaultAsync(d => d.madot == madot);
            var loai = await _context.Loais.FirstOrDefaultAsync(d => d.maloai == maloai);
            var chamcheo = await _context.Chamcheos.FirstOrDefaultAsync(c => c.magv1 == magv || c.magv2 == magv);

            var giangvien1 = await _context.Giangviens.FirstOrDefaultAsync(g => g.magv == chamcheo.magv1);
            var giangvien2 = await _context.Giangviens.FirstOrDefaultAsync(g => g.magv == chamcheo.magv2);

            List<Ketqua> listketqua = await _context.Ketquas
                .Include(k => k.phancong)
                .ThenInclude(p => p.sinhvien)
                .Include(k => k.phancong)
                .ThenInclude(p => p.chitiets)
                .Where(k => k.phancong.magv == magv && k.phancong.madot == madot && k.phancong.maloai == maloai)
                .OrderByDescending(t => t.phancong.sinhvien.ten)
                .ToListAsync();


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
         
                </style> ");

            htmlBuilder.AppendLine($@"
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
                    <p class=""lable""><strong>{dot.name}
                            Hình thức:{loai.name}
                    </p>
                    <p><em>(Lưu ý: nếu loại hình thức ""học kỳ doanh nghiệp"" thì không có giáo viên chấm 2)</em></p>
                </div>
                <div>
                    <table width=""100%"">
                        <tr class=""table-title"">
                            <td><strong>STT</strong></td>
                            <td><strong>Mã số sinh viên|Lớp|Khoa</strong></td>
                            <td><strong>Tên sinh viên</strong></td>");

            if (loai.maloai == "KLTN")
            {

                htmlBuilder.AppendLine(@"<td><strong>Tên đề tài</strong></td>");
                htmlBuilder.AppendLine(@"<td><strong>Điểm cuối cùng</strong></td>");
                htmlBuilder.AppendLine(@" </ tr >");

                for (int i = 0; i < listketqua.Count; i++)
                {
                    Ketqua ketqua = listketqua[i];
                    string? tendetai = ketqua.phancong.chitiets.FirstOrDefault()?.tendetai;

                    double sum = (double)((ketqua.tieuchi1 ?? 0) + (ketqua.tieuchi2 ?? 0) + (ketqua.tieuchi3 ?? 0) + (ketqua.tieuchi4 ?? 0) + (ketqua.tieuchi5 ?? 0) + (ketqua.tieuchi6 ?? 0) + (ketqua.tieuchi7 ?? 0));
                    if (ketqua.phancong.maloai == "HKDN")
                    {
                        sum = (double)(sum * 0.6 + (ketqua.diemDN ?? 0) * 0.4);
                    }
                    if (sum >= 10)
                    {
                        sum = 10;
                    }
                    htmlBuilder.AppendLine("<tr>");
                    htmlBuilder.AppendLine($"<td>{i + 1}</td>");
                    htmlBuilder.AppendLine($"<td>{ketqua.phancong.sinhvien.mssv + "|" + ketqua.phancong.sinhvien.malop + "|" + ketqua.phancong.maloai}</td>");
                    htmlBuilder.AppendLine($"<td>{ketqua.phancong.sinhvien.ho + " " + ketqua.phancong.sinhvien.ten}</td>");
                    htmlBuilder.AppendLine($"<td>{tendetai}</td>");
                    htmlBuilder.AppendLine($"<td>{Math.Round(sum, 2)}</td>");
                    htmlBuilder.AppendLine("</tr>");

                    if (giangvien1.magv == magv)
                    {
                        htmlBuilder.AppendLine(@"</table></div>");
                        htmlBuilder.AppendLine($@"
                        <div>
                            <p><strong>Ngày chấm :</strong></p>
                            <p><strong>Giáo viên hướng dẫn và chấm 1:{giangvien1.tengv}</strong></p>
                            <p><strong>Giáo viên chấm 2:{giangvien2.tengv}</ </strong></p>

                        </div>
                        ");
                    }
                    else
                    {
                        htmlBuilder.AppendLine(@"</table></div>");
                        htmlBuilder.AppendLine($@"
                        <div>
                            <p><strong>Ngày chấm :</strong></p>
                            <p><strong>Giáo viên hướng dẫn và chấm 1:{giangvien2.tengv}</strong></p>
                            <p><strong>Giáo viên chấm 2:{giangvien1.tengv}</ </strong></p>

                        </div>
                        ");
                    }

                }


            }
            else
            {
                htmlBuilder.AppendLine(@"<td><strong>Điểm cuối cùng</strong></td>");
                htmlBuilder.AppendLine(@"</tr>");

                for (int i = 0; i < listketqua.Count; i++)
                {
                    Ketqua ketqua = listketqua[i];
                    string? tendetai = ketqua.phancong.chitiets.FirstOrDefault()?.tendetai;

                    double sum = (double)((ketqua.tieuchi1 ?? 0) + (ketqua.tieuchi2 ?? 0) + (ketqua.tieuchi3 ?? 0) + (ketqua.tieuchi4 ?? 0) + (ketqua.tieuchi5 ?? 0) + (ketqua.tieuchi6 ?? 0) + (ketqua.tieuchi7 ?? 0));
                    if (ketqua.phancong.maloai == "HKDN")
                    {
                        sum = (double)(sum * 0.6 + (ketqua.diemDN ?? 0) * 0.4);
                    }
                    if (sum >= 10)
                    {
                        sum = 10;
                    }
                    htmlBuilder.AppendLine("<tr>");
                    htmlBuilder.AppendLine($"<td>{i + 1}</td>");
                    htmlBuilder.AppendLine($"<td>{ketqua.phancong.sinhvien.mssv + "|" + ketqua.phancong.sinhvien.malop + "|" + ketqua.phancong.maloai}</td>");
                    htmlBuilder.AppendLine($"<td>{ketqua.phancong.sinhvien.ho + " " + ketqua.phancong.sinhvien.ten}</td>");
                    htmlBuilder.AppendLine($"<td>{Math.Round(sum, 2)}</td>");
                    htmlBuilder.AppendLine("</tr>");
                }
                if (giangvien1.magv == magv)
                {
                    htmlBuilder.AppendLine(@"</table></div>");
                    htmlBuilder.AppendLine($@"
                        <div>
                            <p><strong>Ngày chấm :</strong></p>
                            <p><strong>Giáo viên hướng dẫn và chấm 1:{giangvien1.tengv}</strong></p>

                        </div>
                        ");
                }
                else
                {
                    htmlBuilder.AppendLine(@"</table></div>");
                    htmlBuilder.AppendLine($@"
                        <div>
                            <p><strong>Ngày chấm :</strong></p>
                            <p><strong>Giáo viên hướng dẫn và chấm 1:{giangvien2.tengv}</strong></p>

                        </div>
                        ");
                }
            }






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

        public async Task<byte[]?> GeneratePdfBySv(string mssv)
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


            var phancong = await _context.Phancongs
                .Include(p => p.sinhvien)
                .ThenInclude(s => s.chuyennganh)
                .Include(p => p.chitiets)
                .Include(p => p.giangvien)
                .Include(p => p.dot)
                .FirstOrDefaultAsync(p => p.mssv == mssv);


            if (phancong == null)
            {
                return null;
            }
            var ketqua = await _context.Ketquas
                 .Include(k => k.phancong)
                 .FirstOrDefaultAsync(k => k.mapc == phancong.Id);

            if (ketqua == null)
            {
                return null;
            }

            double sum = (double)((ketqua.tieuchi1 ?? 0) + (ketqua.tieuchi2 ?? 0) + (ketqua.tieuchi3 ?? 0) + (ketqua.tieuchi4 ?? 0) + (ketqua.tieuchi5 ?? 0) + (ketqua.tieuchi6 ?? 0) + (ketqua.tieuchi7 ?? 0));
            if (sum >= 10)
            {
                sum = 10;
            }

            // Append the HTML content before the table
            htmlBuilder.AppendLine(@" 
            <style>
                   .title {
                display: flex;
                justify-content: space-between;
                margin: 0 50px 0 50px;

            }

            .content {
                text-align: center;
            }

            .lable {
                font-size: 20px;
            }

            table,
            th,
            td {
                border: 1px solid black;
                border-collapse: collapse;
                margin:0 50px;
        

            }

            .table-title {
                font-size: 18px;
                background-color: rgb(172, 172, 172);
            }

            .lable_chitiet {
                display: flex;
                justify-content: space-between;
                padding: 0 50px 0 50px;
                font-weight: 200;
                height: 40px;
                font-size: 20px;
            }

            .content-chitiet {
                font-weight: 100;
            }

            .itatic {
                font-weight: 100;
                font-style: italic;
    
            }

            .footer {
                float: right;
                text-align: center;
            }    margin: 20px 50px;
    
                </style>");
            htmlBuilder.AppendLine($@"
          

        <body>
            <div class=""title"">
                <div>
                    <p><strong>Trường Đại học Kinh tế Tp. Hồ Chí Minh</strong></p>
                    <p><strong>Khoa Công nghệ Thông tin Kinh doanh</strong></p>
                    <p><strong>Chuyên ngành:{phancong.sinhvien.chuyennganh.tencn}</strong></p>
                </div>
                <div>
                    <p><strong>Cộng Hòa Xã Hội Chủ Nghĩa Việt Nam</strong></p>
                    <p style=""text-align: center;""><strong>Độc lập – Tự do – Hạnh phúc</strong></p>
                    <p><strong>&nbsp;</strong></p>
                </div>

            </div>
            <div class=""content"">

                <p class=""lable""><strong>BẢNG ĐIỂM CHI TIẾT </strong></p>
                <p class=""lable""><strong>THỰC TẬP TỐT NGHIỆP CHO SINH VIÊN </strong></p>
                <p class=""lable""><strong>ĐỢT: {phancong.dot.name}
                </p>
                <p class=""itatic""><em>(Dành cho tất cả hình thức thực tập tốt nghiêp)</em></p>
            </div>
            <div class=""lable_chitiet"">
                <p>Họ tên sinh viên: <strong>{phancong.sinhvien.ho + " " + phancong.sinhvien.ten}</strong>  </p>
                <p class=""lable_chitiet--small"">Mã số sinh viên: <strong>{phancong.mssv}</strong> </p>
            </div>
            <div class=""lable_chitiet"">
                <p>Khoá: <strong>{phancong.sinhvien.khoagoc}</strong>  </p>
                <p class=""lable_chitiet--small"">Lớp: <strong>{phancong.sinhvien.thuoclop}</strong></p>
            </div>
            <div class=""lable_chitiet"">
                <p>Tên khoá luận: <strong>{phancong.chitiets.FirstOrDefault()?.tendetai}</strong> </p>
            </div>
            <div class=""lable_chitiet "">
                <p>Họ tên giáo viên chấm: <strong>{phancong.giangvien.tengv}</strong> </p>
                <p class=""lable_chitiet--small"">Là người hướng dẫn:</p>
            </div>
            <div style=""font-weight: 700; "" class=""lable_chitiet"">
                <p>Điểm thành phần</p>
            </div>
            <div>
                <table  width=""90%"">
                    <tr class=""table-title"">
                        <td><strong>STT</strong></td>
                        <td><strong>Tiêu chí</strong></td>
                        <td><strong>Điểm /điểm tối đa</strong></td>

                    </tr>
                    <tr class=""content-chitiet"">
                        <td>1</td>
                        <td>Về vấn đề được đặt ra hay mục tiêu (dựa trên có hay không, tính rõ ràng, tính hợp thời, tính mức độ
                            cấp thiết, tính mức độ phức tạp,…)</td>
                        <td> {ketqua.tieuchi1}/ 1</td>

                    </tr>
                    <tr class=""content-chitiet"">
                        <td>2</td>
                        <td>
                            <p>Phương pháp giải quyết vấn đề hay phương pháp để đạt mục tiêu:</p>
                            <p>+ Rõ ràng và hợp lý | đúng.</p>
                            <p>+ Mức độ áp dụng kiến thức ngành đã học | tự học.</p>
                            <p>+ Hợp thời đại | thiết thực.</p>
                        </td>
                        <td>{ketqua.tieuchi2}/ 1.5</td>

                    </tr>
                    <tr class=""content-chitiet"">
                        <td>3</td>
                        <td>
                            <p>Kỹ năng giải quyết vấn đề và kết quả đạt được so với mục tiêu, gồm:</p>
                            <p>+ Kỹ năng phân tích nghiệp vụ</p>
                            <p>+ Kỹ năng phân tích mô hình | hệ thống | giải pháp.</p>
                            <p>+ Kỹ năng thiết kế mô hình | hệ thống | giải pháp.</p>
                            <p>+ Kỹ năng thiết kế dữ liệu.p>
                            <p>+ Kỹ năng thu thập và phân tích dữ liệu.</p>
                            <p>+ Kỹ năng lập trình.</p>
                            <p>+ Kỹ năng sử dụng, vận dụng các công cụ công nghệ giải quyết các vấn đề.</p>
                            <p>+ Kỹ năng lập kế hoạch và chiến lược công nghệ.</p>
                            <p>+ Kỹ năng sử dụng, vận dụng các công cụ công nghệ giải quyết các vấn đề.</p>
                            <p>+ …</p>
                            <p class=""itatic"">(Ít nhất phải thể hiện được 3 kỹ năng, mỗi kỹ năng tối đa được 3 điểm)</p>
                        </td>
                        <td>{ketqua.tieuchi3}/5</td>
                    </tr>
                    <tr class=""content-chitiet"">
                        <td>4</td>
                        <td>
                            <p>Mức độ kết quả đạt được so với mục tiêu đã đề ra</p>
                        </td>
                        <td>{ketqua.tieuchi4}/ 1</td>

                    </tr>
                    <tr class=""content-chitiet"">
                        <td>5</td>
                        <td>
                            <p>Cách thức trình bày nội dung</p>
                        </td>
                        <td>{ketqua.tieuchi5}/ 1</td>

                    </tr>
                    <tr class=""content-chitiet"">
                        <td>6</td>
                        <td>
                            <p>Tuân thủ quy định làm thực tập tốt nghiệp (dựa trên thái độ, hành vi, tính chuyên cần, …)</p>
                        </td>
                        <td>{ketqua.tieuchi6}/ 0.5</td>

                    </tr>
                    <tr class=""content-chitiet"">
                        <td>7</td>
                        <td>
                            <p>Điểm cộng thêm cho một số trường hợp đặc biệt:p>
                            <p>+ Bài mang tính mới, giải quyết được và cho kết quả chấp nhận được.</p>
                            <p>+ Có bài báo được đăng trên các tạp chí khoa học.</p>
                        </td>

                        <td>{ketqua.tieuchi7}/ 1</td>

                    </tr>
                    <tr class=""lable "">
                        <td></td>
                        <td>Điểm tổng cộng :</td>
                        <td>{Math.Round(sum, 2)}/10</td>

                    </tr>

           
                </table>

            </div>
            <div class=""footer"">
                <p><strong>Giáo viên chấm</strong></p>
                <p class=""itatic"">(Ký tên và ghi rõ họ tên)</p>

            </div>






        </body>
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
                    if (ketqua.phancong.maloai == "HKDN")
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
                    if (ketqua.phancong.maloai == "HKDN")
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
        public async Task<byte[]?> GenerateZipFileForGv(string madot, string magv)
        {
            var phancongs = await _context.Phancongs
                .Include(p => p.sinhvien)
                .Include(p => p.chitiets)
                .Where(p => p.magv == magv && p.madot == madot)
                .ToListAsync();

            if (phancongs.Count == 0)
            {
                return null;
            }


            MemoryStream zipStream = new MemoryStream();


            using (ZipArchive zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var phancong in phancongs)
                {
                    var mssv = phancong.mssv;


                    byte[] pdfBytes = await GeneratePdfBySv(mssv);

                    if (pdfBytes != null)
                    {

                        var entry = zipArchive.CreateEntry($"{mssv}.pdf", CompressionLevel.Optimal);


                        using (Stream entryStream = entry.Open())
                        {
                            await entryStream.WriteAsync(pdfBytes, 0, pdfBytes.Length);
                        }
                    }
                }
            }


            byte[] zipBytes = zipStream.ToArray();


            zipStream.Dispose();

            return zipBytes;
        }

        public async Task<DiemchitietRequest> DiemChiTietSv(string mssv)
        {

            var sinhvienKhoa = await _context.Sinhviens
              .Include(sk => sk.khoa)
              .FirstOrDefaultAsync(sk => sk.mssv == mssv);

            var phancong = await _context.Phancongs
                .Include(p => p.sinhvien)
                .ThenInclude(s => s.chuyennganh)
                .Include(p => p.chitiets)
                .Include(p => p.giangvien)
                .Include(p => p.dot)
                .FirstOrDefaultAsync(p => p.mssv == mssv);

            if (phancong == null)
            {
                return null;
            }

            var ketqua = await _context.Ketquas
                .Include(k => k.phancong)
                .FirstOrDefaultAsync(k => k.mapc == phancong.Id);

            if (ketqua == null)
            {
                return null;
            }

            double sum = (double)((ketqua.tieuchi1 ?? 0) + (ketqua.tieuchi2 ?? 0) + (ketqua.tieuchi3 ?? 0) + (ketqua.tieuchi4 ?? 0) + (ketqua.tieuchi5 ?? 0) + (ketqua.tieuchi6 ?? 0) + (ketqua.tieuchi7 ?? 0));
            if (sum >= 10)
            {
                sum = 10;
            }

            var diemChiTietRequest = new DiemchitietRequest
            {
                tenkhoa = sinhvienKhoa.khoa.tenkhoa,
                tencn = phancong.sinhvien.chuyennganh.tencn,
                tendot = phancong.dot.name,
                hotensv = phancong.sinhvien.ho + " " + phancong.sinhvien.ten,
                mssv = phancong.mssv,
                lop = phancong.sinhvien.thuoclop,
                khoahoc = phancong.sinhvien.khoahoc,
                tenkl = phancong.chitiets.FirstOrDefault()?.tendetai,
                tengv = phancong.giangvien.tengv,
                tieuchi1 = ketqua.tieuchi1,
                tieuchi2 = ketqua.tieuchi2,
                tieuchi3 = ketqua.tieuchi3,
                tieuchi4 = ketqua.tieuchi4,
                tieuchi5 = ketqua.tieuchi5,
                tieuchi6 = ketqua.tieuchi6,
                tieuchi7 = ketqua.tieuchi7,
                diemDN = ketqua.diemDN,
                diemtong = sum
            };

            return diemChiTietRequest;
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
                .Where(k => k.phancong.magv == magv && k.phancong.madot == madot && k.phancong.maloai == maloai)
                .OrderByDescending(t => t.phancong.sinhvien.ten)
                .ToListAsync();

            // Tạo danh sách kết quả DsDiemGvHuongDanRequest và điền thông tin
            string hotengv1 = "";
            string hotengv2 = "";
            var dsDiemGvHuongDan = new List<DsDiemGvHuongDanRequest>();

            if (loai.maloai == "KLTN")
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
                var dsDiemGvHuongDanRequest = new DsDiemGvHuongDanRequest
                {
                    tenkhoa = khoa?.khoa?.tenkhoa,
                    tendot = dot?.name,
                    tenloai = loai?.name,
                    hotensv = ketqua?.phancong?.sinhvien?.ho + " " + ketqua?.phancong?.sinhvien?.ten,
                    mssv = ketqua?.phancong?.sinhvien?.mssv,
                    tendetai = ketqua?.phancong?.chitiets?.FirstOrDefault()?.tendetai,
                    malop = ketqua?.phancong?.sinhvien?.thuoclop,
                    khoahoc = ketqua.phancong.sinhvien?.khoahoc,
                    hotengv1 = hotengv1,
                    hotengv2 = hotengv2,
                };

                dsDiemGvHuongDan.Add(dsDiemGvHuongDanRequest);
            }

            return dsDiemGvHuongDan;
        }

    }
}
