using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ServiceStack.Web;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ueh.BackendApi.Data.EF;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Helper;
using Ueh.BackendApi.IRepositorys;
using Ueh.BackendApi.Request;

namespace Ueh.BackendApi.Repositorys
{
    public class UserRepository : IUserRepository
    {
        private readonly UehDbContext _context;

        public UserRepository(UehDbContext context)
        {
            _context = context;
        }


        public async Task<string> Decrypt(string encryptedJson)
        {
            string key = "0DiKx5N5PU60jOozndHWHISm/MzAgRjDcfkLXxqDELQ=";
            string iv = "Wo/6qIrrDMAYPlE2aZrmdQ==";
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
        public async Task<User> GetInfoUser(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.userId == id);
        }


        public async Task<bool> UpdateInfoUser(User userupdate, string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.userId == id);
            user.email = userupdate.email;
            user.sdt = userupdate.sdt;

            _context.Users.Update(user);
            return await Save();

        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<int> KiemTraUser(string id)
        {
            bool dot = await _context.Dots.AnyAsync(d => d.status == "true");
            bool dangky = await _context.Dangkys.AnyAsync(k => k.mssv == id);
            bool phancong = await _context.Phancongs.AnyAsync(p => p.mssv == id && p.status == "true");
            bool sinhvien = await _context.Sinhviens.AnyAsync(s => s.mssv == id && s.status == "true");

            if (dot)
            {
                if (phancong)
                {
                    return 1;

                }
                else
                {
                    if (dangky && sinhvien)
                    {
                        return 2;
                    }
                    else
                        return 0;
                }



            }
            return -1;

        }

        public async Task<bool> CreateUser(string encryptedJson)
        {
            var decryptedJson = await Decrypt(encryptedJson);
            var user = JsonConvert.DeserializeObject<User>(decryptedJson);
            _context.Users.Add(user);
            return await Save();
        }
    }
}
