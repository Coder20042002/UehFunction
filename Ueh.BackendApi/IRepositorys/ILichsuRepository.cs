using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface ILichsuRepository
    {
        Task<ICollection<Lichsu>> GetLichsus();
        Task<Lichsu> GetLichsu(Guid mapc);
        Task<ICollection<Lichsu>> GetLichsusOfASinhvien(Guid mapc);
        Task<bool> LichsuExists(Guid mapc, DateTime dateTime);
        Task<bool> CreateLichsu(Lichsu lichsu);
        Task<bool> UpdateLichsu(Lichsu lichsu);
        Task<bool> DeleteLichsu(Lichsu lichsu);
        Task<bool> Save();
    }
}
