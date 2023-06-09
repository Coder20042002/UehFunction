﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ueh.BackendApi.Data.Configuration;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.EF
{
    public class UehDbContext : DbContext
    {
        public UehDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SinhvienConfiguration());
            modelBuilder.ApplyConfiguration(new DotConfiguration());
            modelBuilder.ApplyConfiguration(new GiangVienConfiguration());
            modelBuilder.ApplyConfiguration(new LoaiConfiguration());
            modelBuilder.ApplyConfiguration(new KhoaConfiguration());
            modelBuilder.ApplyConfiguration(new DangKyConfiguration());
            modelBuilder.ApplyConfiguration(new LichsuConfiguration());
            modelBuilder.ApplyConfiguration(new ChuyenNganhConfiguration());
            modelBuilder.ApplyConfiguration(new PhancongConfiguration());
            modelBuilder.ApplyConfiguration(new SinhvienKhoaConfiguration());
            modelBuilder.ApplyConfiguration(new ChitietConfiguration());
            modelBuilder.ApplyConfiguration(new KetquaConfiguration());
            modelBuilder.ApplyConfiguration(new GiangvienKhoaConfiguration());
            modelBuilder.ApplyConfiguration(new UploadResultConfiguration());
            modelBuilder.ApplyConfiguration(new ChamcheoConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());



            //modelBuilder.Entity<IdentityUser<Guid>>().ToTable("AppUser").HasKey(x => x.Id);
            //modelBuilder.Entity<IdentityRole<Guid>>().ToTable("AppRole").HasKey(x => x.Id);
            //modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            //modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            //modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            //modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            //modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            //base.OnModelCreating(modelBuilder);
        }
        public DbSet<Sinhvien> Sinhviens { get; set; }
        public DbSet<Dot> Dots { get; set; }
        public DbSet<Giangvien> Giangviens { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<Dangky> Dangkys { get; set; }
        public DbSet<Lichsu> Lichsus { get; set; }
        public DbSet<Chuyennganh> Chuyennganhs { get; set; }
        public DbSet<Phancong> Phancongs { get; set; }
        public DbSet<SinhvienKhoa> SinhvienKhoas { get; set; }
        public DbSet<Chitiet> Chitiets { get; set; }
        public DbSet<Ketqua> Ketquas { get; set; }
        public DbSet<GiangvienKhoa> GiangvienKhoas { get; set; }
        public DbSet<UploadResult> UploadResults { get; set; }
        public DbSet<Chamcheo> Chamcheos { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
