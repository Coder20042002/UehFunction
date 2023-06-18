using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IChuyennganhRepository
    {
        Task<ICollection<Chuyennganh>> GetChuyennganhs();
        Task<Chuyennganh> GetChuyennganh(string magv);
        Task<Chuyennganh> GetChuyennganhName(string name);
        Task<bool> ChuyennganhExists(string magv);
        Task<bool> CreateChuyennganh(Chuyennganh Chuyennganh);
        Task<bool> UpdateChuyennganh(Chuyennganh Chuyennganh);
        Task<bool> DeleteChuyennganh(Chuyennganh Chuyennganh);
        Task<bool> ImportExcelFile(IFormFile formFile);
        Task<byte[]> ExportToExcel();
        Task<bool> Save();
    }
}
