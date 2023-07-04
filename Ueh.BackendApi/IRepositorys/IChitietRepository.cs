using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IChitietRepository
    {
        Task<ICollection<Chitiet>> GetChitiets();
        Task<Chitiet> GetChitiet(string mssv);
        Task<bool> ChitietExists(Guid mapc);
        Task<bool> UpdateChitiet(Chitiet ketqua, string mssv);
        Task<bool> Save();
    }
}
