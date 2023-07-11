﻿using Microsoft.AspNetCore.Identity;
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
            var dot = await _context.Dots.FirstOrDefaultAsync(d => d.status == "true");
            bool phancong = await _context.Phancongs.AnyAsync(p => p.mssv == id || p.magv == id && p.status == "true");
            int phancongCount = await _context.Phancongs.CountAsync(p => p.madot == dot.madot);

            if (dot != null)
            {
                if (phancong)
                {
                    return 1;

                }
                else
                {

                }



            }
            return -1;

        }

        public async Task<UserRequest> CreateUser(string encryptedJson)
        {

            var decryptedJson = await Decrypt(encryptedJson);
            var userlogin = JsonConvert.DeserializeObject<UserLoginRequest>(decryptedJson);

            bool userexist = await _context.Users.AnyAsync(u => u.userId == userlogin.userId);
            var dot = await _context.Dots.FirstOrDefaultAsync(d => d.status == "true");
            int dotinfo = await KiemTraUser(userlogin.userId);


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

                var userinfo = await _context.Users.FirstOrDefaultAsync(u => u.userId == user.userId);
                if (userinfo.role != "student")
                {
                    var giangvien = await _context.Giangviens.FirstOrDefaultAsync(g => g.magv == user.userId);
                    var giangvienkhoa = await _context.GiangvienKhoas.FirstOrDefaultAsync(k => k.magv == user.userId);
                    var userrequest = new UserRequest
                    {
                        code = userinfo.userId,
                        name = userlogin.name,
                        role = userinfo.role,
                        email = userinfo.email,
                        sdt = userinfo.sdt,
                        makhoa = giangvienkhoa.makhoa,
                        madot = dot.madot,
                        dotinfo = dotinfo

                    };

                    return userrequest;
                }
                else
                {
                    var sinhvien = await _context.Sinhviens.FirstOrDefaultAsync(s => s.mssv == user.userId);
                    var sinhvienkhoa = await _context.SinhvienKhoas.FirstOrDefaultAsync(s => s.mssv == user.userId);

                    var phancong = await _context.Phancongs.FirstOrDefaultAsync(p => p.mssv == user.userId && p.status == "true");

                    if (sinhvienkhoa == null)
                    {
                        var userrequest = new UserRequest
                        {
                            code = userinfo.userId,
                            name = userlogin.name,
                            role = userinfo.role,
                            email = userinfo.email,
                            sdt = userinfo.sdt,
                            makhoa = "",
                            madot = dot.madot,
                            maloai = "",
                            dotinfo = dotinfo

                        };
                        return userrequest;

                    }



                    else
                    {
                        var userrequest = new UserRequest
                        {
                            code = userinfo.userId,
                            name = userlogin.name,
                            role = userinfo.role,
                            email = userinfo.email,
                            sdt = userinfo.sdt,
                            makhoa = sinhvienkhoa.makhoa,
                            madot = dot.madot,
                            maloai = phancong.maloai,
                            dotinfo = dotinfo

                        };
                        return userrequest;

                    }

                }

            }
            else
            {
                var userinfo = await _context.Users.FirstOrDefaultAsync(u => u.userId == userlogin.userId);
                if (userinfo.role != "student")
                {
                    var giangvien = await _context.Giangviens.FirstOrDefaultAsync(g => g.magv == userlogin.userId);
                    var giangvienkhoa = await _context.GiangvienKhoas.FirstOrDefaultAsync(k => k.magv == userlogin.userId);
                    var userrequest = new UserRequest
                    {
                        code = userinfo.userId,
                        name = userlogin.name,
                        role = userinfo.role,
                        email = userinfo.email,
                        sdt = userinfo.sdt,
                        makhoa = giangvienkhoa.makhoa,
                        madot = dot.madot,
                        dotinfo = dotinfo

                    };
                    return userrequest;

                }
                else
                {
                    var sinhvien = await _context.Sinhviens.FirstOrDefaultAsync(s => s.mssv == userlogin.userId);
                    var sinhvienkhoa = await _context.SinhvienKhoas.FirstOrDefaultAsync(s => s.mssv == userlogin.userId);

                    var phancong = await _context.Phancongs.FirstOrDefaultAsync(p => p.mssv == userlogin.userId && p.status == "true");

                    if (sinhvienkhoa == null)
                    {
                        var userrequest = new UserRequest
                        {
                            code = userinfo.userId,
                            name = userlogin.name,
                            role = userinfo.role,
                            email = userinfo.email,
                            sdt = userinfo.sdt,
                            makhoa = "",
                            madot = dot.madot,
                            maloai = "",
                            dotinfo = dotinfo

                        };
                        return userrequest;

                    }



                    else
                    {
                        var userrequest = new UserRequest
                        {
                            code = userinfo.userId,
                            name = userlogin.name,
                            role = userinfo.role,
                            email = userinfo.email,
                            sdt = userinfo.sdt,
                            makhoa = sinhvienkhoa.makhoa,
                            madot = dot.madot,
                            maloai = phancong.maloai,
                            dotinfo = dotinfo

                        };
                        return userrequest;

                    }


                }

            }


        }
    }
}
