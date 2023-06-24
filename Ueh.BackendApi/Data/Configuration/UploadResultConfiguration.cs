using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Configuration
{
    public class UploadResultConfiguration : IEntityTypeConfiguration<UploadResult>
    {
        public void Configure(EntityTypeBuilder<UploadResult> builder)
        {
            builder.ToTable("UploadResults");
            builder.HasKey(u => u.Id);
        }
    }
}
