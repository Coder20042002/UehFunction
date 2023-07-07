using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Helper;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IUserRepository
    {
        Task<User> GetInfoUser(string id);
        Task<bool> UpdateInfoUser(User user, string id);
        Task<int> KiemTraUser(string id);

        Task<bool> Save();

    }
}
