using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Ueh.BackendApi.Data.Entities;


namespace Ueh.BackendApi.Data.Configuration
{
    public class LichsuConfiguration : IEntityTypeConfiguration<Lichsu>
    {
        public void Configure(EntityTypeBuilder<Lichsu> builder)
        {
            builder.ToTable("Lichsus");
            builder.HasKey(ls => new { ls.Id, ls.ngay });

            builder.HasOne(ls => ls.phancong)
                .WithMany(pc => pc.lichsus)
                .HasForeignKey(ls => ls.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
