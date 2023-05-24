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
            builder.ToTable("Dangky");
            builder.HasKey(dk => dk.Id);

            builder.HasOne(dk => dk.sinhvien).WithOne(sv => sv.dangky).HasForeignKey<Dangky>(sv => sv.mssv);
            builder.HasOne(dk => dk.giangvien).WithMany(gv => gv.dangkys).HasForeignKey(dk => dk.magv);
            builder.HasOne(dk => dk.loai).WithMany(l => l.dangkies).HasForeignKey(dk => dk.maloai);

        }
    }
}
