using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Configuration
{
    public class KetquaConfiguration : IEntityTypeConfiguration<Ketqua>
    {
        public void Configure(EntityTypeBuilder<Ketqua> builder)
        {
            builder.ToTable("Ketquas");
            builder.HasKey(ct => new { ct.mapc });

            builder.HasOne(ct => ct.phancong).WithMany(pc => pc.ketquas).HasForeignKey(ct => ct.mapc);
        }
    }
}
