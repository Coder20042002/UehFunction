using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceStack.Web;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ueh.BackendApi.Helper;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.User;

namespace Ueh.BackendApi.Repositorys
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IConfiguration _config;

        public UserRepository(UserManager<IdentityUser<Guid>> userManager, SignInManager<IdentityUser<Guid>> signInManager,
             RoleManager<IdentityRole<Guid>> roleManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;

        }


        public async Task<string> Authencate(string username, string paswword)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> Decrypt(string encryptedJson, string key, string iv)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedJson);
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] ivBytes = Convert.FromBase64String(iv);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream(encryptedBytes))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            return await sr.ReadToEndAsync();
                        }
                    }
                }
            }
        }



        public async Task<string> Encrypt(string json, string key, string iv)
        {
            byte[] encryptedBytes;
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] ivBytes = Convert.FromBase64String(iv);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
                        await cs.WriteAsync(jsonBytes, 0, jsonBytes.Length);
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }


        public async Task<bool> Register(string username, string password, string email, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                return false;
            }
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                return false;
            }

            user = new IdentityUser<Guid>
            {
                Email = email,
                UserName = username,
            };

            var passwordHasher = new PasswordHasher<IdentityUser<Guid>>();
            var hashedPassword = passwordHasher.HashPassword(user, password);
            user.PasswordHash = hashedPassword;

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                // Thêm quyền cho người dùng
                await _userManager.AddToRoleAsync(user, role);
                return true;
            }

            return false;
        }


        public async Task<bool> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return false;
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return true;
        }

        public async Task<bool> UserExist(string username)
        {
            if (await _userManager.FindByNameAsync(username) != null)
                return true;
            return false;
        }
    }

}
