using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IPhanCongRepository
    {
        Task<ICollection<PhanCong>> GetPhanCongs();
        Task<PhanCong> GetPhanCong(string magv);
        Task<bool> PhanCongExists(string magv);
        Task<bool> CreatePhanCong(PhanCong PhanCong);
        Task<bool> UpdatePhanCong(PhanCong PhanCong);
        Task<bool> DeletePhanCong(PhanCong PhanCong);
        Task<bool> ImportExcelFile(IFormFile formFile);
        Task<byte[]> ExportToExcel();
        Task<bool> Save();
    }
}
