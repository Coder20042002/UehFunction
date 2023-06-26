using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Configuration
{
    public class GiangvienKhoaConfiguration : IEntityTypeConfiguration<GiangvienKhoa>
    {
        public void Configure(EntityTypeBuilder<GiangvienKhoa> builder)
        {
            builder.ToTable("GiangvienKhoas");
            builder.HasKey(t => new { t.magv, t.makhoa });

            builder.HasOne(t => t.giangvien).WithMany(pc => pc.giangvienkhoas)
                .HasForeignKey(pc => pc.magv).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.khoa).WithMany(pc => pc.giangvienkhoas)
              .HasForeignKey(pc => pc.makhoa).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
