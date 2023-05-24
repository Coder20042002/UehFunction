using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Configuration
{
    public class ChuyenNganhConfiguration : IEntityTypeConfiguration<Chuyennganh>
    {
        public void Configure(EntityTypeBuilder<Chuyennganh> builder)
        {
            builder.ToTable("Chuyennganhs");
            builder.HasKey(cn => cn.macn);

        }
    }
}
