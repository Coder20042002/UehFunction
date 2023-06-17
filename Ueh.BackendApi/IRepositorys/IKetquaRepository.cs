using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IKetquaRepository
    {
        Task<ICollection<Ketqua>> GetScores();
        Task<Ketqua> GetScores(string mssv);
        Task<bool> ScoresExists(string mssv);
        Task<bool> UpdateScores(Ketqua ketqua);
        Task<ICollection<Ketqua>> GetKetQuaByMaGV(string magv);
        Task<bool> Save();
    }
}
