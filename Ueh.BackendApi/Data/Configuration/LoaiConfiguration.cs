using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Configuration
{
    public class LoaiConfiguration : IEntityTypeConfiguration<Loai>
    {
        public void Configure(EntityTypeBuilder<Loai> builder)
        {
            builder.ToTable("Loais");
            builder.HasKey(l => l.maloai);

        }
    }
}
