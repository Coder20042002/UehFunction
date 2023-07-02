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
    public class DangKyConfiguration : IEntityTypeConfiguration<Dangky>
    {
        public void Configure(EntityTypeBuilder<Dangky> builder)
        {
            builder.ToTable("Dangkys");
            builder.HasKey(dk => new { dk.mssv });

            builder.HasOne(dk => dk.giangvien).WithMany(gv => gv.dangkys).HasForeignKey(dk => dk.magv);
            builder.HasOne(dk => dk.khoa).WithMany(l => l.dangkis).HasForeignKey(dk => dk.makhoa);

        }
    }
}
