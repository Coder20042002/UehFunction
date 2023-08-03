using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ueh.BackendApi.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    tendot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngaybatdau = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ngayketthuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dots", x => x.madot);
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
                    tenloai = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Madot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Giangviens",
                columns: table => new
                {
                    magv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tengv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    chuyenmon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    makhoa = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Giangviens", x => x.magv);
                    table.ForeignKey(
                        name: "FK_Giangviens_Khoas_makhoa",
                        column: x => x.makhoa,
                        principalTable: "Khoas",
                        principalColumn: "makhoa");
                });

            migrationBuilder.CreateTable(
                name: "Sinhviens",
                columns: table => new
                {
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    madot = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    malop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngaysinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    maloai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    makhoa = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    macn = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinhviens", x => new { x.mssv, x.madot });
                    table.ForeignKey(
                        name: "FK_Sinhviens_Chuyennganhs_macn",
                        column: x => x.macn,
                        principalTable: "Chuyennganhs",
                        principalColumn: "macn");
                    table.ForeignKey(
                        name: "FK_Sinhviens_Khoas_makhoa",
                        column: x => x.makhoa,
                        principalTable: "Khoas",
                        principalColumn: "makhoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dangkys",
                columns: table => new
                {
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    madot = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_Dangkys", x => new { x.mssv, x.madot });
                    table.ForeignKey(
                        name: "FK_Dangkys_Giangviens_magv",
                        column: x => x.magv,
                        principalTable: "Giangviens",
                        principalColumn: "magv");
                    table.ForeignKey(
                        name: "FK_Dangkys_Khoas_makhoa",
                        column: x => x.makhoa,
                        principalTable: "Khoas",
                        principalColumn: "makhoa");
                });

            migrationBuilder.CreateTable(
                name: "Phancongs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    mssv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    magv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    madot = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                        name: "FK_Phancongs_Sinhviens_mssv_madot",
                        columns: x => new { x.mssv, x.madot },
                        principalTable: "Sinhviens",
                        principalColumns: new[] { "mssv", "madot" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chitiets",
                columns: table => new
                {
                    mapc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tencty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vitri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    huongdan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chucvu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emailhd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sdthd = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.InsertData(
                table: "Loais",
                columns: new[] { "maloai", "tenloai" },
                values: new object[] { "HKDN", "Học kỳ doanh nghiệp" });

            migrationBuilder.InsertData(
                table: "Loais",
                columns: new[] { "maloai", "tenloai" },
                values: new object[] { "KLTN", "Khoá luận tốt nghiệp" });

            migrationBuilder.CreateIndex(
                name: "IX_Dangkys_magv",
                table: "Dangkys",
                column: "magv");

            migrationBuilder.CreateIndex(
                name: "IX_Dangkys_makhoa",
                table: "Dangkys",
                column: "makhoa");

            migrationBuilder.CreateIndex(
                name: "IX_Giangviens_makhoa",
                table: "Giangviens",
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
                name: "IX_Phancongs_mssv_madot",
                table: "Phancongs",
                columns: new[] { "mssv", "madot" });

            migrationBuilder.CreateIndex(
                name: "IX_Sinhviens_macn",
                table: "Sinhviens",
                column: "macn");

            migrationBuilder.CreateIndex(
                name: "IX_Sinhviens_makhoa",
                table: "Sinhviens",
                column: "makhoa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chamcheos");

            migrationBuilder.DropTable(
                name: "Chitiets");

            migrationBuilder.DropTable(
                name: "Dangkys");

            migrationBuilder.DropTable(
                name: "Ketquas");

            migrationBuilder.DropTable(
                name: "Lichsus");

            migrationBuilder.DropTable(
                name: "Loais");

            migrationBuilder.DropTable(
                name: "UploadResults");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Phancongs");

            migrationBuilder.DropTable(
                name: "Dots");

            migrationBuilder.DropTable(
                name: "Giangviens");

            migrationBuilder.DropTable(
                name: "Sinhviens");

            migrationBuilder.DropTable(
                name: "Chuyennganhs");

            migrationBuilder.DropTable(
                name: "Khoas");
        }
    }
}
