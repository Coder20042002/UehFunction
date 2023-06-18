using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Configuration
{
    public class SinhvienKhoaConfiguration : IEntityTypeConfiguration<SinhvienKhoa>
    {
        public void Configure(EntityTypeBuilder<SinhvienKhoa> builder)
        {
            builder.ToTable("SinhvienKhoas");
            builder.HasKey(t => new { t.mssv, t.makhoa });

            builder.HasOne(t => t.sinhvien).WithMany(pc => pc.sinhvienkhoas)
                .HasForeignKey(pc => pc.mssv).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.khoa).WithMany(pc => pc.sinhvienKhoas)
              .HasForeignKey(pc => pc.makhoa).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
