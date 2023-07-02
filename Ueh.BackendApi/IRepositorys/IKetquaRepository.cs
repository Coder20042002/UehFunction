using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IKetquaRepository
    {
        Task<ICollection<Ketqua>> GetScores();
        Task<Ketqua> GetScores(Guid mapc);
        Task<bool> ScoresExists(Guid mapc);
        Task<bool> UpdateScores(Ketqua ketqua);
        Task<ICollection<Ketqua>> GetKetQuaByMaGV(string magv);
        Task<byte[]> ExportToExcel();
        Task<byte[]> ExportToExcelByKhoa(string makhoa);
        Task<byte[]> GeneratePdfByGv(string magv);
        Task<byte[]> GeneratePdfBySv(string mssv);
        Task<byte[]> GenerateZipFileForGv(string magv);
        Task<bool> Save();
    }
}
