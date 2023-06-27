using System.ComponentModel.DataAnnotations;

namespace Ueh.BackendApi.User
{
    public class RegisterRequest
    {
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Email { get; set; }

    }
}
