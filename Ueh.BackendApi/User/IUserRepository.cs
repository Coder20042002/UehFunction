using Ueh.BackendApi.Helper;
using Ueh.BackendApi.User.Model;

namespace Ueh.BackendApi.User
{
    public interface IUserRepository
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);
    }
}
