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
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewerConfiguration());
            modelBuilder.ApplyConfiguration(new SinhVienLoaiConfiguration());
            modelBuilder.ApplyConfiguration(new SinhvienDotConfiguration());
            modelBuilder.ApplyConfiguration(new ChuyenNganhConfiguration());
            modelBuilder.ApplyConfiguration(new PhancongConfiguration());



            //modelBuilder.Seed();
            //base.OnModelCreating(modelBuilder);
        }
        public DbSet<Sinhvien> Sinhviens { get; set; }
        public DbSet<Dot> Dots { get; set; }
        public DbSet<Giangvien> Giangviens { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<Dangky> Dankys { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<SinhvienLoai> SinhvienLoai { get; set; }
        public DbSet<SinhvienDot> SinhvienDot { get; set; }
        public DbSet<Chuyennganh> Chuyennganhs { get; set; }

        public DbSet<PhanCong> Phancongs { get; set; }


    }
}
