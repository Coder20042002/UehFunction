﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ueh.BackendApi.Data.EF;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    [DbContext(typeof(UehDbContext))]
    [Migration("20230707041457_Add_Dot")]
    partial class Add_Dot
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppRole", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppUser", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AppUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserTokens", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Chamcheo", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("madot")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("magv1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("magv2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("makhoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Chamcheos", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Chitiet", b =>
                {
                    b.Property<Guid>("mapc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("chucvu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("emailsv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("huongdan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sdt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("stdhd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tencty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tendetai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("vitri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("mapc");

                    b.ToTable("Chitiets", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Chuyennganh", b =>
                {
                    b.Property<string>("macn")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("makhoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tencn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("macn");

                    b.ToTable("Chuyennganhs", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Dangky", b =>
                {
                    b.Property<string>("mssv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lop")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("madot")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("magv")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("makhoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("mssv");

                    b.HasIndex("magv");

                    b.HasIndex("makhoa");

                    b.ToTable("Dangkys", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Dot", b =>
                {
                    b.Property<string>("madot")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ngaybatdau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ngayketthuc")
                        .HasColumnType("datetime2");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("madot");

                    b.ToTable("Dots", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Giangvien", b =>
                {
                    b.Property<string>("magv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("chuyenmon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sdt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tengv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("magv");

                    b.ToTable("Giangviens", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.GiangvienKhoa", b =>
                {
                    b.Property<string>("magv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("makhoa")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("magv", "makhoa");

                    b.HasIndex("makhoa");

                    b.ToTable("GiangvienKhoas", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Ketqua", b =>
                {
                    b.Property<Guid>("mapc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float?>("diemDN")
                        .HasColumnType("real");

                    b.Property<float?>("tieuchi1")
                        .HasColumnType("real");

                    b.Property<float?>("tieuchi2")
                        .HasColumnType("real");

                    b.Property<float?>("tieuchi3")
                        .HasColumnType("real");

                    b.Property<float?>("tieuchi4")
                        .HasColumnType("real");

                    b.Property<float?>("tieuchi5")
                        .HasColumnType("real");

                    b.Property<float?>("tieuchi6")
                        .HasColumnType("real");

                    b.Property<float?>("tieuchi7")
                        .HasColumnType("real");

                    b.HasKey("mapc");

                    b.ToTable("Ketquas", (string)null);
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

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Lichsu", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ngay")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("noidung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "ngay");

                    b.ToTable("Lichsus", (string)null);
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

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Phancong", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("madot")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("magv")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("maloai")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("mssv")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("madot");

                    b.HasIndex("magv");

                    b.HasIndex("maloai");

                    b.HasIndex("mssv");

                    b.ToTable("Phancongs", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Sinhvien", b =>
                {
                    b.Property<string>("mssv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("bacdt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khoagoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("khoahoc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loaihinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("macn")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("madot")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mahp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("malhp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("malop")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("soct")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tenhp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("thuoclop")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("mssv");

                    b.HasIndex("macn");

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

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.SinhvienKhoa", b =>
                {
                    b.Property<string>("mssv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("makhoa")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("mssv", "makhoa");

                    b.HasIndex("makhoa");

                    b.ToTable("SinhvienKhoas", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.UploadResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mssv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoredFileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UploadResults", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Chitiet", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Phancong", "phancong")
                        .WithMany("chitiets")
                        .HasForeignKey("mapc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("phancong");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Dangky", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Giangvien", "giangvien")
                        .WithMany("dangkys")
                        .HasForeignKey("magv")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Khoa", "khoa")
                        .WithMany("dangkis")
                        .HasForeignKey("makhoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("giangvien");

                    b.Navigation("khoa");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.GiangvienKhoa", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Giangvien", "giangvien")
                        .WithMany("giangvienkhoas")
                        .HasForeignKey("magv")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Khoa", "khoa")
                        .WithMany("giangvienkhoas")
                        .HasForeignKey("makhoa")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("giangvien");

                    b.Navigation("khoa");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Ketqua", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Phancong", "phancong")
                        .WithMany("ketquas")
                        .HasForeignKey("mapc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("phancong");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Lichsu", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Phancong", "phancong")
                        .WithMany("lichsus")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("phancong");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Phancong", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Dot", "dot")
                        .WithMany("phanCongs")
                        .HasForeignKey("madot")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Giangvien", "giangvien")
                        .WithMany("phancongs")
                        .HasForeignKey("magv")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Loai", "loai")
                        .WithMany("phanCongs")
                        .HasForeignKey("maloai")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Sinhvien", "sinhvien")
                        .WithMany("phancongs")
                        .HasForeignKey("mssv")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("dot");

                    b.Navigation("giangvien");

                    b.Navigation("loai");

                    b.Navigation("sinhvien");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Sinhvien", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Chuyennganh", "chuyennganh")
                        .WithMany("sinhviens")
                        .HasForeignKey("macn");

                    b.Navigation("chuyennganh");
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

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.SinhvienKhoa", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Khoa", "khoa")
                        .WithMany("sinhvienkhoas")
                        .HasForeignKey("makhoa")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Sinhvien", "sinhvien")
                        .WithMany("sinhvienkhoas")
                        .HasForeignKey("mssv")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("khoa");

                    b.Navigation("sinhvien");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Chuyennganh", b =>
                {
                    b.Navigation("sinhviens");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Dot", b =>
                {
                    b.Navigation("phanCongs");

                    b.Navigation("sinhviendots");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Giangvien", b =>
                {
                    b.Navigation("dangkys");

                    b.Navigation("giangvienkhoas");

                    b.Navigation("phancongs");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Khoa", b =>
                {
                    b.Navigation("dangkis");

                    b.Navigation("giangvienkhoas");

                    b.Navigation("sinhvienkhoas");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Loai", b =>
                {
                    b.Navigation("phanCongs");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Phancong", b =>
                {
                    b.Navigation("chitiets");

                    b.Navigation("ketquas");

                    b.Navigation("lichsus");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Sinhvien", b =>
                {
                    b.Navigation("phancongs");

                    b.Navigation("sinhviendots");

                    b.Navigation("sinhvienkhoas");
                });
#pragma warning restore 612, 618
        }
    }
}
