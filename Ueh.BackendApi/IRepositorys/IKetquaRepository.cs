using Microsoft.AspNetCore.Mvc;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IKetquaRepository
    {
        Task<ICollection<Ketqua>> GetScores();
        Task<Ketqua> GetDiemByMssv(string mssv);
        Task<bool> ScoresExists(Guid mapc);
        Task<bool> UpdateDiem(Ketqua updateketqua, string mssv);
        Task<ICollection<Ketqua>> GetKetQuaByMaGV(string magv);
        Task<byte[]> ExportToExcel();
        Task<byte[]> ExportToExcelByKhoa(string madot, string makhoa);
        Task<byte[]> GeneratePdfByGv(string magv);
        Task<byte[]> GeneratePdfBySv(string mssv);
        Task<byte[]> GenerateZipFileForGv(string magv);
        Task<bool> Save();
    }
}
