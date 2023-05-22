﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ueh.BackendApi.Data.EF;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    [DbContext(typeof(UehDbContext))]
    partial class UehDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Dangky", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("dotmadot")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("madot")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("magv")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("maloai")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("mssv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("dotmadot");

                    b.HasIndex("magv");

                    b.HasIndex("maloai");

                    b.ToTable("Dangky", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Dot", b =>
                {
                    b.Property<string>("madot")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("dateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dateStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("madot");

                    b.ToTable("Dots", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Giangvien", b =>
                {
                    b.Property<string>("magv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("makhoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("tengv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("magv");

                    b.HasIndex("makhoa");

                    b.ToTable("Giangviens", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Khoa", b =>
                {
                    b.Property<string>("makhoa")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("tenkhoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("makhoa");

                    b.ToTable("Khoas", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Loai", b =>
                {
                    b.Property<string>("maloai")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("maloai");

                    b.ToTable("Loais", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Review", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"), 1L, 1);

                    b.Property<DateTime>("ngay")
                        .HasColumnType("datetime2");

                    b.Property<string>("noidung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("reviewerid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("sinhvienmssv")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("reviewerid");

                    b.HasIndex("sinhvienmssv");

                    b.ToTable("Reviews", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Reviewer", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("hoten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Reviewers", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Sinhvien", b =>
                {
                    b.Property<string>("mssv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Khoamakhoa")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("chuyennganh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hoten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sdt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tenlop")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("mssv");

                    b.HasIndex("Khoamakhoa");

                    b.ToTable("Sinhviens", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.SinhvienDot", b =>
                {
                    b.Property<string>("mssv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("madot")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("mssv", "madot");

                    b.HasIndex("madot");

                    b.ToTable("SinhvienDots", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.SinhvienLoai", b =>
                {
                    b.Property<string>("mssv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("maloai")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("mssv", "maloai");

                    b.HasIndex("maloai");

                    b.ToTable("SinhvienLoais", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Dangky", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Dot", "dot")
                        .WithMany()
                        .HasForeignKey("dotmadot")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Giangvien", "giangvien")
                        .WithMany("dangkies")
                        .HasForeignKey("magv")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Loai", "loai")
                        .WithMany("dangkies")
                        .HasForeignKey("maloai")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("dot");

                    b.Navigation("giangvien");

                    b.Navigation("loai");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Giangvien", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Khoa", "khoa")
                        .WithMany("giangviens")
                        .HasForeignKey("makhoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_GangVien_Khoa");

                    b.Navigation("khoa");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Review", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Reviewer", "reviewer")
                        .WithMany("reviews")
                        .HasForeignKey("reviewerid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Sinhvien", "sinhvien")
                        .WithMany("reviews")
                        .HasForeignKey("sinhvienmssv")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("reviewer");

                    b.Navigation("sinhvien");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Sinhvien", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Khoa", null)
                        .WithMany("students")
                        .HasForeignKey("Khoamakhoa");

                    b.HasOne("Ueh.BackendApi.Data.Entities.Dangky", "dangky")
                        .WithOne("sinhvien")
                        .HasForeignKey("Ueh.BackendApi.Data.Entities.Sinhvien", "mssv")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("dangky");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.SinhvienDot", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Dot", "dot")
                        .WithMany("sinhviendots")
                        .HasForeignKey("madot")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Sinhvien", "sinhvien")
                        .WithMany("sinhviendots")
                        .HasForeignKey("mssv")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("dot");

                    b.Navigation("sinhvien");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.SinhvienLoai", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Loai", "loai")
                        .WithMany("sinhvienloais")
                        .HasForeignKey("maloai")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Sinhvien", "sinhvien")
                        .WithMany("sinhvienloais")
                        .HasForeignKey("mssv")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("loai");

                    b.Navigation("sinhvien");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Dangky", b =>
                {
                    b.Navigation("sinhvien")
                        .IsRequired();
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Dot", b =>
                {
                    b.Navigation("sinhviendots");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Giangvien", b =>
                {
                    b.Navigation("dangkies");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Khoa", b =>
                {
                    b.Navigation("giangviens");

                    b.Navigation("students");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Loai", b =>
                {
                    b.Navigation("dangkies");

                    b.Navigation("sinhvienloais");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Reviewer", b =>
                {
                    b.Navigation("reviews");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Sinhvien", b =>
                {
                    b.Navigation("reviews");

                    b.Navigation("sinhviendots");

                    b.Navigation("sinhvienloais");
                });
#pragma warning restore 612, 618
        }
    }
}
