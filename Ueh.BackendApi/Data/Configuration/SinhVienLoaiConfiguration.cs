using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Configuration
{
    public class SinhVienLoaiConfiguration : IEntityTypeConfiguration<SinhvienLoai>
    {
        public void Configure(EntityTypeBuilder<SinhvienLoai> builder)
        {
            builder.ToTable("SinhvienLoais");
            builder.HasKey(t => new { t.mssv, t.maloai });


            builder.HasOne(t => t.loai).WithMany(pc => pc.sinhvienloais)
              .HasForeignKey(pc => pc.maloai).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
