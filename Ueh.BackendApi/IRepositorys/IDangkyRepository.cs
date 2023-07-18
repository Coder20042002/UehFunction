using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Dtos;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IDangkyRepository
    {

        Task<List<GiangvienRequest>> GetGiangvienListFromDangky(string madot, string makhoa);
        Task<List<Dangky>> GetSinhVienByGiaoVien(string madot, string makhoa, string magv);
        Task<ICollection<Dangky>> GetDangkys();
        Task<Dangky> GetDangky(string madot, string mssv);
        Task<bool> DangkyExists(string magv);
        Task<bool> CreateDangky(Dangky Dangky);
        Task<bool> UpdateDangky(Dangky Dangky);
        Task<bool> DeleteDangky(string madot, string mssv);
        Task<bool> ImportExcelFile(IFormFile formFile, string madot, string makhoa, string magv);
        Task<byte[]> ExportToExcel(string madot, string makhoa);
        Task<bool> Save();
    }
}
