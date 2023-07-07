using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IChitietRepository
    {
        Task<ICollection<Chitiet>> GetChitiets();
        Task<ChitietRequest> GetChitiet(string mssv);
        Task<bool> ChitietExists(Guid mapc);
        Task<bool> UpdateChitiet(ChitietRequest ketqua, string mssv);
        Task<bool> Save();
    }
}
