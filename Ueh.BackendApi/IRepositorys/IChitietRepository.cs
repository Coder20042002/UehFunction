using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IChitietRepository
    {
        Task<ICollection<Chitiet>> GetChitiets();
        Task<Chitiet> GetChitiet(Guid mapc);
        Task<bool> ChitietExists(Guid mapc);
        Task<bool> UpdateChitiet(Chitiet ketqua);
        Task<bool> Save();
    }
}
