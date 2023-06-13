using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IPhancongRepository
    {
        Task<ICollection<Phancong>> GetPhancongs();
        Task<Phancong> GetPhancong(string magv);
        Task<bool> PhancongExists(string magv);
        Task<bool> CreatePhancong(Phancong Phancong);
        Task<bool> UpdatePhancong(Phancong Phancong);
        Task<bool> DeletePhancong(Phancong Phancong);
        Task<bool> ImportExcelFile(IFormFile formFile);
        Task<byte[]> ExportToExcel();
        Task<bool> Save();
    }
}
