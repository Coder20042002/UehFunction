using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.IRepositorys
{
    public interface ILichsuRepository
    {
        Task<ICollection<Lichsu>> GetLichSuByMssv(string madot, string mssv);
        Task<ICollection<Lichsu>> GetLichsus();
        Task<Lichsu> GetLichsu(Guid mapc);
        Task<ICollection<Lichsu>> GetLichsusOfASinhvien(Guid mapc);
        Task<bool> CreateLichsu(LichsuRequest lichsu, string mssv);
        Task<bool> UpdateLichsu(Lichsu lichsu);
        Task<bool> DeleteLichsu(Lichsu lichsu);
        Task<bool> Save();
    }
}
