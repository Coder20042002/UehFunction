using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Helper;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.IRepositorys
{
    public interface IUserRepository
    {
        Task<bool> CreateUser(string encryptedJson);
        Task<string> Encrypt(string json, string key, string iv);
        Task<string> Decrypt(string encryptedJson);
        Task<User> GetInfoUser(string id);
        Task<bool> UpdateInfoUser(User user, string id);
        Task<int> KiemTraUser(string id);
        Task<bool> Save();

    }
}
