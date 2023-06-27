using Ueh.BackendApi.Helper;
using Ueh.BackendApi.User;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IUserRepository
    {
        Task<string> Authencate(string username, string paswword);
        Task<bool> Register(string username, string password, string email, string role);
        Task<bool> RoleAssign(Guid id, RoleAssignRequest request);
        Task<bool> UserExist(string username);
    }
}
