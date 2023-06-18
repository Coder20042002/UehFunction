﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Configuration
{
    public class ChitietConfiguration : IEntityTypeConfiguration<Chitiet>
    {
        public void Configure(EntityTypeBuilder<Chitiet> builder)
        {
            builder.ToTable("Chitiets");
            builder.HasKey(ct => new { ct.mapc, ct.mssv });

            builder.HasOne(ct => ct.sinhvien).WithMany(sv => sv.chitiets).HasForeignKey(ct => ct.mssv);
            builder.HasOne(ct => ct.phancong).WithMany(pc => pc.chitiets).HasForeignKey(ct => ct.mapc);

        }
    }
}
