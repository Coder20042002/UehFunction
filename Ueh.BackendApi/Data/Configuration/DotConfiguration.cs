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
    public class DotConfiguration : IEntityTypeConfiguration<Dot>
    {
        public void Configure(EntityTypeBuilder<Dot> builder)
        {
            builder.ToTable("Dots");
            builder.HasKey(x => x.madot);
            builder.Property(x => x.dateStart).IsRequired();
            builder.Property(x => x.dateEnd).IsRequired();


        }
    }
}
