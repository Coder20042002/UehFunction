using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    }
}
