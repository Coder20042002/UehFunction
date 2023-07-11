using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Helper;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IUserRepository
    {
        Task<UserRequest> CreateUser(string encryptedJson);
        Task<string> Encrypt(string json, string key, string iv);
        Task<string> Decrypt(string encryptedJson);
        Task<User> GetInfoUser(string id);
        Task<bool> UpdateInfoUser(User user, string id);
        Task<int> getDotInfo(string userId, string role);
        Task<bool> Save();

    }
}
