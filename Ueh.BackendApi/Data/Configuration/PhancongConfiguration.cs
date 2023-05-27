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
        }
    }
}
