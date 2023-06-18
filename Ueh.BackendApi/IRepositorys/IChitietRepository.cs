using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IChitietRepository
    {
        Task<ICollection<Chitiet>> GetChitiet();
        Task<Chitiet> GetChitiet(string mssv);
        Task<bool> ChitietExists(string mssv);
        Task<bool> UpdateChitiet(Chitiet ketqua);
        Task<bool> Save();
    }
}
