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

                    b.HasData(
                        new
                        {
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            RoleId = new Guid("8d04dce2-969a-435d-bba4-df3f325983dc")
                        });
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

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                            ConcurrencyStamp = "2facf446-63fc-4615-b936-d18d6cc0e926",
                            Description = "Administrator role",
                            Name = "admin",
                            NormalizedName = "admin"
                        },
                        new
                        {
                            Id = new Guid("63e7e1bd-88ea-498e-be49-823ea3952484"),
                            ConcurrencyStamp = "82fa29a0-1fda-4329-957f-48dcd04e00f4",
                            Description = "Studentistrator role",
                            Name = "student",
                            NormalizedName = "student"
                        },
                        new
                        {
                            Id = new Guid("3686da9d-db16-48ab-a9b2-aafb842a9fcc"),
                            ConcurrencyStamp = "28bc86df-895d-4fc4-a904-78c1142acf2e",
                            Description = "Tearchistrator role",
                            Name = "tearch",
                            NormalizedName = "tearch"
                        });
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.AppUser", b =>
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

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

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

                    b.ToTable("AppUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "07f5e432-6281-4236-b28f-b0b27512b52d",
                            Email = "phuong123@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Phương",
                            LastName = "Trần Hoài",
                            LockoutEnabled = false,
                            NormalizedEmail = "phuong123@gmail.com",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEEfssU+xQn028NYq91byGcjfSQQVL56fSrx3E22WAeHyhomEFTy1ykcLaKtsNA2Jwg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Chitiet", b =>
                {
                    b.Property<Guid>("mapc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("mssv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("chucvu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
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

                    b.HasKey("mapc", "mssv");

                    b.HasIndex("mssv");

                    b.ToTable("Chitiets", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Chuyennganh", b =>
                {
                    b.Property<string>("macn")
                        .HasColumnType("nvarchar(450)");

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

                    b.Property<string>("magv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("maloai")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("hotensv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("mssv", "magv", "maloai");

                    b.HasIndex("magv");

                    b.HasIndex("maloai");

                    b.ToTable("Dangkys", (string)null);
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

                    b.Property<string>("macn")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("makhoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tengv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("magv");

                    b.HasIndex("macn");

                    b.HasIndex("makhoa");

                    b.ToTable("Giangviens", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Ketqua", b =>
                {
                    b.Property<Guid>("mapc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("mssv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("tieuchi1")
                        .HasColumnType("int");

                    b.Property<int>("tieuchi2")
                        .HasColumnType("int");

                    b.Property<int>("tieuchi3")
                        .HasColumnType("int");

                    b.Property<int>("tieuchi4")
                        .HasColumnType("int");

                    b.Property<int>("tieuchi5")
                        .HasColumnType("int");

                    b.Property<int>("tieuchi6")
                        .HasColumnType("int");

                    b.Property<int>("tieuchi7")
                        .HasColumnType("int");

                    b.HasKey("mapc", "mssv");

                    b.HasIndex("mssv");

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

                    b.Property<string>("macn")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

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

                    b.HasKey("Id");

                    b.HasIndex("macn");

                    b.HasIndex("madot");

                    b.HasIndex("magv");

                    b.HasIndex("maloai");

                    b.HasIndex("mssv");

                    b.ToTable("Phancongs", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Review", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

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

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hoten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ngaysinh")
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

                    b.ToTable("Sinhviens", (string)null);
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.SinhvienChuyenNganh", b =>
                {
                    b.Property<string>("mssv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("macn")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("mssv", "macn");

                    b.HasIndex("macn");

                    b.ToTable("SinhvienChuyenNganhs", (string)null);
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

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Chitiet", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Phancong", "phancong")
                        .WithMany("chitiets")
                        .HasForeignKey("mapc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Sinhvien", "sinhvien")
                        .WithMany("chitiets")
                        .HasForeignKey("mssv")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("phancong");

                    b.Navigation("sinhvien");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Dangky", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Giangvien", "giangvien")
                        .WithMany("dangkys")
                        .HasForeignKey("magv")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Loai", "loai")
                        .WithMany("dangkies")
                        .HasForeignKey("maloai")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("giangvien");

                    b.Navigation("loai");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Giangvien", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Chuyennganh", "chuyennganh")
                        .WithMany("giangviens")
                        .HasForeignKey("macn")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_GangVien_ChuyenNganh");

                    b.HasOne("Ueh.BackendApi.Data.Entities.Khoa", "khoa")
                        .WithMany("giangviens")
                        .HasForeignKey("makhoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_GangVien_Khoa");

                    b.Navigation("chuyennganh");

                    b.Navigation("khoa");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Ketqua", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Phancong", "phancong")
                        .WithMany("ketquas")
                        .HasForeignKey("mapc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Sinhvien", "sinhvien")
                        .WithMany("ketquas")
                        .HasForeignKey("mssv")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("phancong");

                    b.Navigation("sinhvien");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Phancong", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Chuyennganh", "chuyennganh")
                        .WithMany("phanCongs")
                        .HasForeignKey("macn")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Dot", "dot")
                        .WithMany("phanCongs")
                        .HasForeignKey("madot")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Giangvien", "Giangvien")
                        .WithMany("phancongs")
                        .HasForeignKey("magv")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Loai", "loai")
                        .WithMany("phanCongs")
                        .HasForeignKey("maloai")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Sinhvien", "Sinhvien")
                        .WithMany("phancongs")
                        .HasForeignKey("mssv")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Giangvien");

                    b.Navigation("Sinhvien");

                    b.Navigation("chuyennganh");

                    b.Navigation("dot");

                    b.Navigation("loai");
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

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.SinhvienChuyenNganh", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Chuyennganh", "chuyennganh")
                        .WithMany("sinhvienChuyenNganhs")
                        .HasForeignKey("macn")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Sinhvien", "sinhvien")
                        .WithMany("sinhvienchuyennganhs")
                        .HasForeignKey("mssv")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("chuyennganh");

                    b.Navigation("sinhvien");
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
                        .WithMany("sinhvienKhoas")
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

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.SinhvienLoai", b =>
                {
                    b.HasOne("Ueh.BackendApi.Data.Entities.Loai", "loai")
                        .WithMany("sinhvienloais")
                        .HasForeignKey("maloai")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ueh.BackendApi.Data.Entities.Sinhvien", "sinhvien")
                        .WithMany()
                        .HasForeignKey("mssv")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("loai");

                    b.Navigation("sinhvien");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Chuyennganh", b =>
                {
                    b.Navigation("giangviens");

                    b.Navigation("phanCongs");

                    b.Navigation("sinhvienChuyenNganhs");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Dot", b =>
                {
                    b.Navigation("phanCongs");

                    b.Navigation("sinhviendots");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Giangvien", b =>
                {
                    b.Navigation("dangkys");

                    b.Navigation("phancongs");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Khoa", b =>
                {
                    b.Navigation("giangviens");

                    b.Navigation("sinhvienKhoas");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Loai", b =>
                {
                    b.Navigation("dangkies");

                    b.Navigation("phanCongs");

                    b.Navigation("sinhvienloais");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Phancong", b =>
                {
                    b.Navigation("chitiets");

                    b.Navigation("ketquas");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Reviewer", b =>
                {
                    b.Navigation("reviews");
                });

            modelBuilder.Entity("Ueh.BackendApi.Data.Entities.Sinhvien", b =>
                {
                    b.Navigation("chitiets");

                    b.Navigation("ketquas");

                    b.Navigation("phancongs");

                    b.Navigation("reviews");

                    b.Navigation("sinhvienchuyennganhs");

                    b.Navigation("sinhviendots");

                    b.Navigation("sinhvienkhoas");
                });
#pragma warning restore 612, 618
        }
    }
}
