using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Engines;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.EF
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = new Guid("63E7E1BD-88EA-498E-BE49-823EA3952484"),
                Name = "student",
                NormalizedName = "student",
                Description = "Studentistrator role"
            });

            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = new Guid("3686DA9D-DB16-48AB-A9B2-AAFB842A9FCC"),
                Name = "teacher",
                NormalizedName = "teacher",
                Description = "Tearchistrator role"
            });
            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "phuong123@gmail.com",
                NormalizedEmail = "phuong123@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FirstName = "Phương",
                LastName = "Trần Hoài",
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
