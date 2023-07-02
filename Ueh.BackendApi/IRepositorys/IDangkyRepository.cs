using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IDangkyRepository
    {
        Task<List<Dangky>> GetSinhVienByGiaoVien(string magv);
        Task<ICollection<Dangky>> GetDangkys();
        Task<Dangky> GetDangky(string magv);
        Task<bool> DangkyExists(string magv);
        Task<bool> CreateDangky(Dangky Dangky);
        Task<bool> UpdateDangky(Dangky Dangky);
        Task<bool> DeleteDangky(Dangky Dangky);
        Task<bool> ImportExcelFile(IFormFile formFile, string makhoa, string magv);
        Task<byte[]> ExportToExcel();
        Task<bool> Save();
    }
}
