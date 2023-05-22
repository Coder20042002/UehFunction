using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Configuration
{
    public class SinhvienDotConfiguration : IEntityTypeConfiguration<SinhvienDot>
    {
        public void Configure(EntityTypeBuilder<SinhvienDot> builder)
        {
            builder.ToTable("SinhvienDots");
            builder.HasKey(t => new { t.mssv, t.madot });

            builder.HasOne(t => t.sinhvien).WithMany(pc => pc.sinhviendots)
                .HasForeignKey(pc => pc.mssv).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.dot).WithMany(pc => pc.sinhviendots)
              .HasForeignKey(pc => pc.madot).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
