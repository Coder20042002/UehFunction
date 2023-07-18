using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IPhancongRepository
    {
        Task<bool> UpdateLoaiHinhThucTap(string mssv, string maloai);
        Task<bool> KiemTraMaloai(string mssv);
        Task<ICollection<Phancong>> GetPhancongKhoas(string madot, string makhoa);
        Task<Phancong> GetPhancong(string magv);
        Task<bool> PhancongExists(string mssv);
        Task<bool> CreatePhancong(PhancongRequest Phancong);
        Task<bool> UpdatePhancong(string mssv, string magv);
        Task<bool> DeletePhancong(string mssv);
        Task<bool> ImportExcelFile(IFormFile formFile, string madot);
        Task<byte[]> ExportToExcel(string madot, string makhoa);
        Task<ICollection<Phancong>> SearchByTenSinhVien(string tenSinhVien);
        Task<ICollection<Phancong>> GetPhanCongByMaGV(string magv);
        Task<bool> Save();
    }
}
