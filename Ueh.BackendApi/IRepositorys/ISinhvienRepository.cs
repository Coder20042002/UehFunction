using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;

namespace Ueh.BackendApi.IRepositorys
{
    public interface ISinhvienRepository
    {
        Task<ICollection<Sinhvien>> GetSinhviens();
        Task<Sinhvien> GetSinhvien(string mssv);
        Task<Sinhvien> GetSinhvienName(string name);
        Task<Sinhvien> GetSinhvienTrimToUpper(SinhvienDto sinhvienCreate);
        Task<bool> SinhvienExists(string mssv);
        Task<bool> CreateSinhvien(string madot, string makhoa, Sinhvien sinhvien);
        Task<bool> UpdateSinhvien(string madot, string makhoa, Sinhvien sinhvien);
        Task<bool> DeleteSinhvien(Sinhvien sinhvien);
        Task<bool> ImportExcelFile(string madot, string makhoa, IFormFile formFile);
        Task<byte[]> ExportToExcel();
        Task<bool> Save();
    }
}
