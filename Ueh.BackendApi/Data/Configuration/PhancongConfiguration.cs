using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Configuration
{
    public class PhancongConfiguration : IEntityTypeConfiguration<PhanCong>
    {
        public void Configure(EntityTypeBuilder<PhanCong> builder)
        {
            builder.ToTable("Phancongs");
            builder.HasKey(pc => pc.Id);

            builder.HasOne(pc => pc.Sinhvien).WithMany(sv => sv.phanCongs).HasForeignKey(pc => pc.mssv).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(pc => pc.loai).WithMany(l => l.phanCongs).HasForeignKey(pc => pc.maloai).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(pc => pc.Giangvien).WithMany(gv => gv.phanCongs).HasForeignKey(pc => pc.magv).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(pc => pc.dot).WithMany(d => d.phanCongs).HasForeignKey(pc => pc.madot).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(pc => pc.chuyennganh).WithMany(cn => cn.phanCongs).HasForeignKey(pc => pc.macn).OnDelete(DeleteBehavior.NoAction);


        }
    }
}
