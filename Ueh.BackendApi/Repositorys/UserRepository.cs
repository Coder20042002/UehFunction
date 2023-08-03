using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OfficeOpenXml;
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


        public async Task<bool> CreateUserRoleAdmin(string makhoa, GiangvienUpdateRequest giangvienupdate)
        {

            var kiemtrauser = await _context.Users.FirstOrDefaultAsync(g => g.userId == giangvienupdate.magv);
            var kiemtragv = await _context.Giangviens.FirstOrDefaultAsync(g => g.magv == giangvienupdate.magv);

            if (kiemtragv == null)
            {


                var giangvien = new Giangvien
                {
                    magv = giangvienupdate.magv,
                    makhoa = makhoa,
                    tengv = giangvienupdate.tengv,
                    status = "true"

                };
                await _context.Giangviens.AddAsync(giangvien);
                if (kiemtrauser == null)
                {
                    var user = new User
                    {
                        userId = giangvienupdate.magv,
                        email = giangvienupdate.email,
                        sdt = giangvienupdate.sdt,
                        role = "admin"

                    };
                    _context.Add(user);
                }

            }
            else
            {
                kiemtrauser.role = "admin";
                _context.Users.Update(kiemtrauser);
            }




            return await Save();



        }

        public async Task<List<UserRoleAdminRequest>> GetUserRoleAdminRequests()
        {
            var userRoleAdminRequests = await _context.Users
                .Where(user => user.role == "admin")
                .Select(user => new UserRoleAdminRequest
                {
                    userId = user.userId,
                    email = user.email,
                    sdt = user.sdt,
                    role = user.role,
                    name = _context.Giangviens
                        .Where(gv => gv.magv == user.userId && gv.status == "true")
                        .Select(gv => gv.tengv)
                        .FirstOrDefault() ?? "", // Nếu giảng viên không tồn tại, giá trị mặc định là ""
                    tenkhoa = _context.Giangviens
                        .Where(gv => gv.magv == user.userId && gv.status == "true")
                        .Select(gv => gv.khoa.tenkhoa)
                        .FirstOrDefault() ?? "" // Nếu khoa không tồn tại, giá trị mặc định là ""
                })
                .ToListAsync();

            return userRoleAdminRequests;
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


        public async Task<bool> UpdateInfoUser(UpdateUserRequest userupdate, string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.userId == id);
            user.email = userupdate.email;
            user.sdt = userupdate.sdt;

            _context.Users.Update(user);
            return await Save();

        }

        public async Task<bool> DeleteRoleAdmin(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.userId == id);
            user.role = "teacher";

            _context.Users.Update(user);
            return await Save();

        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<int> GetDotInfo(string userId, string role)
        {
            var dot = await _context.Dots.FirstOrDefaultAsync(d => d.status == "true");
            if (dot == null)
            {
                // Chưa mở đợt
                return 0;
            }
            else
            {
                bool phancong = await _context.Phancongs.AnyAsync(p => (p.mssv == userId || p.magv == userId) && p.madot == dot.madot && p.status == "true");
                int phancongCount = await _context.Phancongs.CountAsync(p => p.madot == dot.madot && p.status == "true");

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
            int dotInfoValue = await GetDotInfo(userinfo.userId, userinfo.role);

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
                var sinhvien = await _context.Sinhviens.FirstOrDefaultAsync(s => s.mssv == userlogin.userId);
                if (sinhvien != null)
                {
                    var phancong = await _context.Phancongs.FirstOrDefaultAsync(p => p.mssv == userlogin.userId && p.madot == dot.madot && p.status == "true");
                    userrequest.makhoa = sinhvien.makhoa;
                    userrequest.maloai = phancong != null ? phancong.sinhvien.maloai : "";
                }
            }
            else
            {
                var giangvien = await _context.Giangviens.FirstOrDefaultAsync(k => k.magv == userlogin.userId);
                if (giangvien != null)
                {
                    userrequest.makhoa = giangvien.makhoa;
                };
            }

            return userrequest;

        }

        public async Task<bool> ImportExcelFile(IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    formFile.CopyTo(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var makhoa = worksheet.Cells[row, 5].Value?.ToString();
                            bool existingkhoa = await _context.Khoas.AnyAsync(s => s.makhoa == makhoa);

                            var magv = worksheet.Cells[row, 1].Value?.ToString();
                            bool existing = await _context.Giangviens.AnyAsync(g => g.magv == magv && g.status == "true");

                            if (existing == true || existingkhoa == false)
                            {
                                continue;
                            }
                            var giangvien = new Giangvien
                            {
                                magv = magv,
                                tengv = worksheet.Cells[row, 2].Value?.ToString(),
                                makhoa = makhoa
                            };

                            var kiemtrauser = await _context.Users.AnyAsync(g => g.userId == magv);

                            if (kiemtrauser == false)
                            {
                                var user = new User
                                {
                                    userId = magv,
                                    email = worksheet.Cells[row, 4].Value?.ToString(),
                                    sdt = worksheet.Cells[row, 3].Value?.ToString(),
                                    role = "admin"

                                };
                                _context.Add(user);

                            }





                            await _context.Giangviens.AddAsync(giangvien);
                        }

                        return await Save();
                    }
                }

            }
            return false;
        }

        public async Task<bool> UserExists(string userId)
        {
            return await _context.Users.AnyAsync(s => s.userId == userId);
        }
    }
}
