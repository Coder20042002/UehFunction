using ServiceStack;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IDotRepository
    {
        Task<ICollection<Dot>> GetAllDot();
        Task<Dot> GetDot();
        Task<bool> DotExists(string id);
        Task<bool> CreateDot(Dot dot);
        Task<bool> UpdateDot(Dot dot);
        Task<bool> DeleteDot(Dot dot);
        Task<bool> Save();
    }
}
