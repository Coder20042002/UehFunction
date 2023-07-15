using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.IRepositorys
{
    public interface ISinhvienRepository
    {
        Task<List<SinhvienInfoRequest>> SearchSinhVien(string keyword);
        Task<string> GetLoaiHinhThucTap(string mssv);
        Task<Giangvien> GetGvHuongDanSv(string mssv);
        Task<ICollection<Sinhvien>> GetSinhviens();
        Task<Sinhvien> GetSinhvien(string mssv);
        Task<Khoa> GetKhoaBySinhvien(string mssv);
        Task<List<SinhvienInfoRequest>> GetDsSinhvienOfKhoa(string madot, string makhoa);
        Task<Sinhvien> GetSinhvienName(string name);
        Task<bool> SinhvienExists(string mssv);
        Task<bool> CreateSinhvien(string makhoa, Sinhvien sinhvien);
        Task<bool> UpdateSinhvien(Sinhvien sinhvien);
        Task<bool> DeleteSinhvien(Sinhvien sinhvien);
        Task<bool> ImportExcelFile(IFormFile formFile, string madot, string makhoa);
        Task<bool> Save();
    }
}
