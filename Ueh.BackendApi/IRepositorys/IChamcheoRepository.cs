using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IChamcheoRepository
    {
        Task<bool> ImportExcelFile(IFormFile formFile, string madot, string makhoa);
        Task<bool> UpdateChamcheo(Chamcheo chamcheo);
        Task<bool> Save();

    }
}
