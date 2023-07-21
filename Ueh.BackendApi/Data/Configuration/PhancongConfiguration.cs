﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ueh.BackendApi.Data.Entities;

namespace Ueh.BackendApi.Data.Configuration
{
    public class PhancongConfiguration : IEntityTypeConfiguration<Phancong>
    {
        public void Configure(EntityTypeBuilder<Phancong> builder)
        {
            builder.ToTable("Phancongs");
            builder.HasKey(pc => pc.Id);

            builder.HasOne(pc => pc.sinhvien).WithMany(sv => sv.phancongs).HasForeignKey(pc => pc.mssv).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(pc => pc.loai).WithMany(l => l.phanCongs).HasForeignKey(pc => pc.maloai).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(pc => pc.giangvien).WithMany(gv => gv.phancongs).HasForeignKey(pc => pc.magv).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(pc => pc.dot).WithMany(d => d.phanCongs).HasForeignKey(pc => pc.madot).OnDelete(DeleteBehavior.NoAction);



        }
    }
}
