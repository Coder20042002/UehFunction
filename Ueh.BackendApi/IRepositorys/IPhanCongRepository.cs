using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IPhancongRepository
    {
        Task<bool> KiemTraMaloai(string mssv);
        Task<ICollection<Phancong>> GetPhancongs();
        Task<Phancong> GetPhancong(string magv);
        Task<bool> PhancongExists(string mssv);
        Task<bool> CreatePhancong(Phancong Phancong);
        Task<bool> UpdatePhancong(Phancong Phancong);
        Task<bool> DeletePhancong(Phancong Phancong);
        Task<bool> ImportExcelFile(IFormFile formFile);
        Task<byte[]> ExportToExcel(string madot,string makhoa);
        Task<ICollection<Phancong>> SearchByTenSinhVien(string tenSinhVien);
        Task<ICollection<Phancong>> GetPhanCongByMaGV(string magv);
        Task<bool> Save();
    }
}
