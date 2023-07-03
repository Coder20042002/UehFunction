using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Chamcheos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    magv1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    magv2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    makhoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    madot = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamcheos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Chuyennganhs",
                columns: table => new
                {
                    macn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tencn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    makhoa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chuyennganhs", x => x.macn);
                });

            migrationBuilder.CreateTable(
                name: "Dots",
                columns: table => new
                {
                    madot = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngaybatdau = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ngayketthuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dots", x => x.madot);
                });

            migrationBuilder.CreateTable(
                name: "Giangviens",
                columns: table => new
                {
                    magv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tengv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    chuyenmon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Giangviens", x => x.magv);
                });

            migrationBuilder.CreateTable(
                name: "Khoas",
                columns: table => new
                {
                    makhoa = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tenkhoa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoas", x => x.makhoa);
                });

            migrationBuilder.CreateTable(
                name: "Loais",
                columns: table => new
                {
                    maloai = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loais", x => x.maloai);
                });

            migrationBuilder.CreateTable(
                name: "UploadResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mssv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoredFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sinhviens",
                columns: table => new
                {
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    thuoclop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    khoagoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    khoahoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mahp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    malhp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenhp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    soct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    malop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bacdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    loaihinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    macn = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinhviens", x => x.mssv);
                    table.ForeignKey(
                        name: "FK_Sinhviens_Chuyennganhs_macn",
                        column: x => x.macn,
                        principalTable: "Chuyennganhs",
                        principalColumn: "macn");
                });

            migrationBuilder.CreateTable(
                name: "Dangkys",
                columns: table => new
                {
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    magv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    makhoa = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dangkys", x => x.mssv);
                    table.ForeignKey(
                        name: "FK_Dangkys_Giangviens_magv",
                        column: x => x.magv,
                        principalTable: "Giangviens",
                        principalColumn: "magv",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dangkys_Khoas_makhoa",
                        column: x => x.makhoa,
                        principalTable: "Khoas",
                        principalColumn: "makhoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiangvienKhoas",
                columns: table => new
                {
                    magv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    makhoa = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangvienKhoas", x => new { x.magv, x.makhoa });
                    table.ForeignKey(
                        name: "FK_GiangvienKhoas_Giangviens_magv",
                        column: x => x.magv,
                        principalTable: "Giangviens",
                        principalColumn: "magv",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GiangvienKhoas_Khoas_makhoa",
                        column: x => x.makhoa,
                        principalTable: "Khoas",
                        principalColumn: "makhoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Phancongs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    magv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    madot = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    maloai = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phancongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phancongs_Dots_madot",
                        column: x => x.madot,
                        principalTable: "Dots",
                        principalColumn: "madot");
                    table.ForeignKey(
                        name: "FK_Phancongs_Giangviens_magv",
                        column: x => x.magv,
                        principalTable: "Giangviens",
                        principalColumn: "magv");
                    table.ForeignKey(
                        name: "FK_Phancongs_Loais_maloai",
                        column: x => x.maloai,
                        principalTable: "Loais",
                        principalColumn: "maloai");
                    table.ForeignKey(
                        name: "FK_Phancongs_Sinhviens_mssv",
                        column: x => x.mssv,
                        principalTable: "Sinhviens",
                        principalColumn: "mssv");
                });

            migrationBuilder.CreateTable(
                name: "SinhvienDots",
                columns: table => new
                {
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    madot = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhvienDots", x => new { x.mssv, x.madot });
                    table.ForeignKey(
                        name: "FK_SinhvienDots_Dots_madot",
                        column: x => x.madot,
                        principalTable: "Dots",
                        principalColumn: "madot",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SinhvienDots_Sinhviens_mssv",
                        column: x => x.mssv,
                        principalTable: "Sinhviens",
                        principalColumn: "mssv",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SinhvienKhoas",
                columns: table => new
                {
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    makhoa = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhvienKhoas", x => new { x.mssv, x.makhoa });
                    table.ForeignKey(
                        name: "FK_SinhvienKhoas_Khoas_makhoa",
                        column: x => x.makhoa,
                        principalTable: "Khoas",
                        principalColumn: "makhoa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SinhvienKhoas_Sinhviens_mssv",
                        column: x => x.mssv,
                        principalTable: "Sinhviens",
                        principalColumn: "mssv",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chitiets",
                columns: table => new
                {
                    mapc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tencty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vitri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sdt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    huongdan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chucvu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stdhd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tendetai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chitiets", x => x.mapc);
                    table.ForeignKey(
                        name: "FK_Chitiets_Phancongs_mapc",
                        column: x => x.mapc,
                        principalTable: "Phancongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ketquas",
                columns: table => new
                {
                    mapc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tieuchi1 = table.Column<float>(type: "real", nullable: true),
                    tieuchi2 = table.Column<float>(type: "real", nullable: true),
                    tieuchi3 = table.Column<float>(type: "real", nullable: true),
                    tieuchi4 = table.Column<float>(type: "real", nullable: true),
                    tieuchi5 = table.Column<float>(type: "real", nullable: true),
                    tieuchi6 = table.Column<float>(type: "real", nullable: true),
                    tieuchi7 = table.Column<float>(type: "real", nullable: true),
                    diemDN = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ketquas", x => x.mapc);
                    table.ForeignKey(
                        name: "FK_Ketquas_Phancongs_mapc",
                        column: x => x.mapc,
                        principalTable: "Phancongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lichsus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ngay = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    noidung = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lichsus", x => new { x.Id, x.ngay });
                    table.ForeignKey(
                        name: "FK_Lichsus_Phancongs_Id",
                        column: x => x.Id,
                        principalTable: "Phancongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dangkys_magv",
                table: "Dangkys",
                column: "magv");

            migrationBuilder.CreateIndex(
                name: "IX_Dangkys_makhoa",
                table: "Dangkys",
                column: "makhoa");

            migrationBuilder.CreateIndex(
                name: "IX_GiangvienKhoas_makhoa",
                table: "GiangvienKhoas",
                column: "makhoa");

            migrationBuilder.CreateIndex(
                name: "IX_Phancongs_madot",
                table: "Phancongs",
                column: "madot");

            migrationBuilder.CreateIndex(
                name: "IX_Phancongs_magv",
                table: "Phancongs",
                column: "magv");

            migrationBuilder.CreateIndex(
                name: "IX_Phancongs_maloai",
                table: "Phancongs",
                column: "maloai");

            migrationBuilder.CreateIndex(
                name: "IX_Phancongs_mssv",
                table: "Phancongs",
                column: "mssv");

            migrationBuilder.CreateIndex(
                name: "IX_SinhvienDots_madot",
                table: "SinhvienDots",
                column: "madot");

            migrationBuilder.CreateIndex(
                name: "IX_SinhvienKhoas_makhoa",
                table: "SinhvienKhoas",
                column: "makhoa");

            migrationBuilder.CreateIndex(
                name: "IX_Sinhviens_macn",
                table: "Sinhviens",
                column: "macn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRole");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "Chamcheos");

            migrationBuilder.DropTable(
                name: "Chitiets");

            migrationBuilder.DropTable(
                name: "Dangkys");

            migrationBuilder.DropTable(
                name: "GiangvienKhoas");

            migrationBuilder.DropTable(
                name: "Ketquas");

            migrationBuilder.DropTable(
                name: "Lichsus");

            migrationBuilder.DropTable(
                name: "SinhvienDots");

            migrationBuilder.DropTable(
                name: "SinhvienKhoas");

            migrationBuilder.DropTable(
                name: "UploadResults");

            migrationBuilder.DropTable(
                name: "Phancongs");

            migrationBuilder.DropTable(
                name: "Khoas");

            migrationBuilder.DropTable(
                name: "Dots");

            migrationBuilder.DropTable(
                name: "Giangviens");

            migrationBuilder.DropTable(
                name: "Loais");

            migrationBuilder.DropTable(
                name: "Sinhviens");

            migrationBuilder.DropTable(
                name: "Chuyennganhs");
        }
    }
}
