using Microsoft.AspNetCore.Identity;

namespace Ueh.BackendApi.Data.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
