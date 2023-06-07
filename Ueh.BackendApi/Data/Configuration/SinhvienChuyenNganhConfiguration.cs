using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Configuration
{
    public class SinhvienChuyenNganhConfiguration : IEntityTypeConfiguration<SinhvienChuyenNganh>
    {
        public void Configure(EntityTypeBuilder<SinhvienChuyenNganh> builder)
        {
            builder.ToTable("SinhvienChuyenNganhs");
            builder.HasKey(t => new { t.mssv, t.macn });

            builder.HasOne(t => t.sinhvien).WithMany(pc => pc.sinhvienchuyennganhs)
                .HasForeignKey(pc => pc.mssv).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.chuyennganh).WithMany(pc => pc.sinhvienChuyenNganhs)
              .HasForeignKey(pc => pc.macn).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
