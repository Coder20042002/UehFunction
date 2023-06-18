using Microsoft.AspNetCore.Identity;

namespace Ueh.BackendApi.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
