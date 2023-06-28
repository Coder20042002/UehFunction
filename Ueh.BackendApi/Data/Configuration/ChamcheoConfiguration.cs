using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Configuration
{
    public class ChamcheoConfiguration : IEntityTypeConfiguration<Chamcheo>
    {
        public void Configure(EntityTypeBuilder<Chamcheo> builder)
        {
            builder.ToTable("Chamcheos");

            builder.HasKey(c => c.id);
        }
    }
}
