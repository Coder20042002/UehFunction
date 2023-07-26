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
    public class SinhvienConfiguration : IEntityTypeConfiguration<Sinhvien>
    {
        public void Configure(EntityTypeBuilder<Sinhvien> builder)
        {
            builder.ToTable("Sinhviens");
            builder.HasKey(s => new { s.mssv, s.madot });

            builder.HasOne(s => s.chuyennganh).WithMany(k => k.sinhviens).HasForeignKey(s => s.macn);
            builder.HasOne(dk => dk.khoa).WithMany(gv => gv.sinhviens).HasForeignKey(dk => dk.makhoa);

        }
    }
}
