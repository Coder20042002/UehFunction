using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IChuyennganhRepository
    {
        Task<ICollection<Chuyennganh>> GetChuyennganhsByKhoa(string makhoa);
        Task<Chuyennganh> GetChuyennganhById(string magv);
        Task<bool> ChuyennganhExists(string magv);
        Task<bool> CreateChuyennganh(Chuyennganh Chuyennganh);
        Task<bool> UpdateChuyennganh(Chuyennganh Chuyennganh);
        Task<bool> DeleteChuyennganh(Chuyennganh Chuyennganh);
        Task<bool> ImportExcelFile(IFormFile formFile, string makhoa);
        Task<byte[]> ExportToExcel();
        Task<bool> Save();
    }
}
