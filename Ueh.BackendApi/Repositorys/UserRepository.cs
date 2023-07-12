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

        public async Task<int> getDotInfo(string userId, string role)
        {
            var dot = await _context.Dots.FirstOrDefaultAsync(d => d.status == "true");
            if (dot == null)
            {
                // Chưa mở đợt
                return 0;
            }
            else
            {
                bool phancong = await _context.Phancongs.AnyAsync(p => p.mssv == userId || p.magv == userId && p.status == "true");
                int phancongCount = await _context.Phancongs.CountAsync(p => p.madot == dot.madot);

                if (role == "admin")
                {
                    return phancongCount > 0 ? 2 : 1;
                }
                else
                {
                    return phancong ? 2 : 1;
                }
            }
        }

        public async Task<UserRequest> CreateUser(string encryptedJson)
        {
            // Giải mã và lấy thông tin từ token
            var decryptedJson = await Decrypt(encryptedJson);
            var userlogin = JsonConvert.DeserializeObject<UserLoginRequest>(decryptedJson);

            bool userexist = await _context.Users.AnyAsync(u => u.userId == userlogin.userId);

            // Kiểm tra tồn tại user, nếu chưa, thêm vào database
            if (!userexist)
            {
                var user = new User
                {
                    userId = userlogin.userId,
                    email = userlogin.email,
                    sdt = userlogin.sdt,
                    role = userlogin.role
                };
                _context.Users.Add(user);
                await Save();
            }

            // Lấy user từ database
            var userinfo = await _context.Users.FirstOrDefaultAsync(u => u.userId == userlogin.userId);

            var dot = await _context.Dots.FirstOrDefaultAsync(d => d.status == "true");
            int dotInfoValue = await getDotInfo(userinfo.userId, userinfo.role);

            // Khởi tạo user ban đầu
            var userrequest = new UserRequest
            {
                code = userinfo.userId,
                name = userlogin.name,
                role = userinfo.role,
                email = userinfo.email,
                sdt = userinfo.sdt,
                madot = dotInfoValue == 0 ? null : dot.madot,
                dotinfo = dotInfoValue
            };

            if (userinfo.role == "student")
            {
                var sinhvienkhoa = await _context.SinhvienKhoas.FirstOrDefaultAsync(s => s.mssv == userlogin.userId);
                if (sinhvienkhoa != null)
                {
                    var phancong = await _context.Phancongs.FirstOrDefaultAsync(p => p.mssv == userlogin.userId && p.status == "true");
                    var sinhvien = await _context.Sinhviens.FirstOrDefaultAsync(s => s.mssv == userlogin.userId);

                    userrequest.makhoa = sinhvienkhoa.makhoa;
                    userrequest.maloai = phancong.maloai;
                }
            }
            else
            {
                var giangvienkhoa = await _context.GiangvienKhoas.FirstOrDefaultAsync(k => k.magv == userlogin.userId);
                if (giangvienkhoa != null)
                {
                    userrequest.makhoa = giangvienkhoa.makhoa;
                };
            }

            return userrequest;

        }
    }
}
