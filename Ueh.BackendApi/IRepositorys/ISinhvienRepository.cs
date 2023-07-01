using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;

namespace Ueh.BackendApi.IRepositorys
{
    public interface ISinhvienRepository
    {
        Task<ICollection<Sinhvien>> GetSinhviens();
        Task<Sinhvien> GetSinhvien(string mssv);
        Task<Khoa> GetKhoaBySinhvien(string mssv);
        Task<Sinhvien> GetSinhvienName(string name);
        Task<bool> SinhvienExists(string mssv);
        Task<bool> CreateSinhvien(string makhoa, Sinhvien sinhvien);
        Task<bool> UpdateSinhvien(Sinhvien sinhvien);
        Task<bool> DeleteSinhvien(Sinhvien sinhvien);
        Task<bool> ImportExcelFile(string makhoa, IFormFile formFile);
        Task<bool> Save();
    }
}
