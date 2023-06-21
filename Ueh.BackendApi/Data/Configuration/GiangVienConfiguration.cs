using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Configuration
{
    public class GiangVienConfiguration : IEntityTypeConfiguration<Giangvien>
    {
        public void Configure(EntityTypeBuilder<Giangvien> builder)
        {
            builder.ToTable("Giangviens");
            builder.HasKey(x => x.magv);


        }
    }
}
