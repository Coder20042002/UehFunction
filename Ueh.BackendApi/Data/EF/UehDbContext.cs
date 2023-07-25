using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ueh.BackendApi.Data.Configuration;
using Ueh.BackendApi.Data.Entities;
using Ueh.BackendApi.Data.Extensions;

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
            modelBuilder.ApplyConfiguration(new ChitietConfiguration());
            modelBuilder.ApplyConfiguration(new KetquaConfiguration());
            modelBuilder.ApplyConfiguration(new UploadResultConfiguration());
            modelBuilder.ApplyConfiguration(new ChamcheoConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Seed();


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
        public DbSet<Chitiet> Chitiets { get; set; }
        public DbSet<Ketqua> Ketquas { get; set; }
        public DbSet<UploadResult> UploadResults { get; set; }
        public DbSet<Chamcheo> Chamcheos { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
