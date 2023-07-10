using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IChamcheoRepository
    {
        Task<byte[]> ExportToExcel(string madot, string makhoa);
        Task<List<ChamcheoRequest>> GetChamcheoByGiangVien(string madot, string makhoa);
        Task<bool> ImportExcelFile(IFormFile formFile, string madot, string makhoa);
        Task<bool> UpdateChamcheo(Chamcheo chamcheo);
        Task<bool> Save();

    }
}
