using System.ComponentModel.DataAnnotations;

namespace Ueh.BackendApi.Request
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }

        [Display(Name = "Tên")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        public string LastName { get; set; }

        [Display(Name = "Hòm thư")]
        public string Email { get; set; }

    }
}